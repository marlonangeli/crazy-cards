using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Security.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CrazyCards.Application.Core.Player.Commands;

internal sealed class LoginHandler : ICommandHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly KeycloakSettings _keycloakSettings;

    public LoginHandler(IDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _keycloakSettings = configuration.GetSection(KeycloakSettings.SectionName).Get<KeycloakSettings>()!;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Set<Domain.Entities.Player.Player>()
            .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
        if (user is null)
        {
            return Result.Failure<LoginResponse>(new Error("Player.NotFound", "Jogador não encontrado"));
        }

        var httpClient = new HttpClient();
        string authUrl =
            $"{_keycloakSettings.AuthServerUrl}/realms/{_keycloakSettings.Realm}/protocol/openid-connect/token";

        var authContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", _keycloakSettings.Resource),
            new KeyValuePair<string, string>("client_secret", _keycloakSettings.Credentials.Secret),
            new KeyValuePair<string, string>("username", request.Username),
            new KeyValuePair<string, string>("password", request.Password),
        });
        
        var authResponse = await httpClient.PostAsync(authUrl, authContent, cancellationToken);
        if (!authResponse.IsSuccessStatusCode)
        {
            return Result.Failure<LoginResponse>(new Error("Player.InvalidCredentials", "Credenciais inválidas"));
        }
        
        var authResponseContent = await authResponse.Content.ReadAsStringAsync(cancellationToken);

        var tokenResponse = JsonSerializer.Deserialize<LoginResponse>(authResponseContent);
        return Result.Success(tokenResponse)!;
    }
}