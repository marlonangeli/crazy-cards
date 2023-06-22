using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Security.Settings;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.Extensions.Configuration;

namespace CrazyCards.Application.Core.Player.Commands;

internal sealed class CreatePlayerHandler : ICommandHandler<CreatePlayerCommand, Result<PlayerResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IKeycloakClient _keycloakClient;
    private readonly KeycloakSettings _keycloakSettings;
    private readonly IMapper _mapper;

    public CreatePlayerHandler(
        IDbContext dbContext,
        IKeycloakClient keycloakClient,
        IMapper mapper,
        IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _keycloakClient = keycloakClient;
        _keycloakSettings = configuration.GetSection(KeycloakSettings.SectionName).Get<KeycloakSettings>()!;
    }

    public async Task<Result<PlayerResponse>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var user = new User
        {
            Id = id.ToString(),
            Email = request.Email,
            Username = request.Username,
            Credentials = new[]
            {
                new Credential
                {
                    Type = "password",
                    Value = request.Password,
                    SecretData = request.Password,
                    CredentialData = request.Password,
                    Temporary = false
                }
            },
            Enabled = true,
        };
        
        // TODO - Erro 403 ao criar usuário no Keycloak
        // var keycloakResponse = await _keycloakClient.CreateUser(_keycloakSettings.Realm, user);
        // if (keycloakResponse is not { IsSuccessStatusCode: true })
        // {
        //     return Result.Failure<PlayerResponse>(new Error(
        //         "Player.CreatePlayer",
        //         "Falha ao criar o usuário no Keycloak"));
        // }

        var player = new Domain.Entities.Player.Player
        {
            Id = id,
            Username = request.Username,
            Email = request.Email,
            PlayerDeck = new PlayerDeck
            {
                Id = Guid.NewGuid(),
                PlayerId = id,
            }
        };

        var entity = await _dbContext.Set<Domain.Entities.Player.Player>().AddAsync(player, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlayerResponse>(entity.Entity);
    }
}