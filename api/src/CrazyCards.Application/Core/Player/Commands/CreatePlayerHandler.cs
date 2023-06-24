using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Security.Settings;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Application.Core.Player.Commands;

internal sealed class CreatePlayerHandler : ICommandHandler<CreatePlayerCommand, Result<PlayerResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IKeycloakUserClient _keycloakClient;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePlayerHandler> _logger;
    private readonly string Realm;

    public CreatePlayerHandler(
        IDbContext dbContext,
        IMapper mapper,
        IKeycloakUserClient keycloakClient, 
        ILogger<CreatePlayerHandler> logger,
        IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _keycloakClient = keycloakClient;
        _logger = logger;
        Realm = configuration.GetSection(KeycloakSettings.SectionName).Get<KeycloakSettings>()!.Realm;
    }

    public async Task<Result<PlayerResponse>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        var player = new Domain.Entities.Player.Player
        {
            Id = id,
            Username = request.Username,
            Email = request.Email
        };

        var playerDeck = new PlayerDeck
        {
            Id = Guid.NewGuid(),
            PlayerId = id,
        };

        var user = new User
        {
            Id = id.ToString(),
            Username = request.Username,
            Email = request.Email,
            Enabled = true,
            Credentials = new[]
            {
                new Credential
                {
                    Type = "password",
                    Value = request.Password,
                    Temporary = false
                }
            }
        };

        var keycloakResponse = await _keycloakClient.CreateUser(Realm, user);
        _logger.LogDebug("Keycloak response: {@StatusCode}", keycloakResponse.StatusCode);
        
        if (!keycloakResponse.IsSuccessStatusCode)
            return Result.Failure<PlayerResponse>(
                new Error("Player.CreatePlayer", "Falha ao criar usuário no Keycloak"));

        var entity = await _dbContext.Set<Domain.Entities.Player.Player>().AddAsync(player, cancellationToken);
        await _dbContext.Set<PlayerDeck>().AddAsync(playerDeck, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlayerResponse>(entity.Entity);
    }
}