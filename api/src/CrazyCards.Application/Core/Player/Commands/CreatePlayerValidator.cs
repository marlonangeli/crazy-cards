using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Security.Settings;
using FluentValidation;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CrazyCards.Application.Core.Player.Commands;

public sealed class CreatePlayerValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerValidator(
        IDbContext dbContext,
        IKeycloakClient keycloakClient,
        IConfiguration configuration)
    {
        var realm = configuration.GetSection(KeycloakSettings.SectionName).Get<KeycloakSettings>()!.Realm;

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Player.EmailCannotBeEmpty)
            .EmailAddress()
            .WithError(ValidationErrors.Player.InvalidEmail)
            .MustAsync(async (email, cancellationToken) =>
                !await dbContext.Set<Domain.Entities.Player.Player>()
                    .AnyAsync(p => p.Email == email, cancellationToken))
            .WithError(ValidationErrors.Player.EmailExists);

        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Player.UsernameCannotBeEmpty)
            .MustAsync(async (username, cancellationToken) =>
            {
                var player = await dbContext.Set<Domain.Entities.Player.Player>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Username == username, cancellationToken);
                if (player is null)
                    return true;

                return false;
                
                // TODO - Erro 403
                // var user = await keycloakClient.GetUser(realm, player.Id.ToString());
                // return user is null;
            })
            .WithError(ValidationErrors.Player.UserExists)
            .MustAsync(async (username, cancellationToken) =>
                !await dbContext.Set<Domain.Entities.Player.Player>()
                    .AnyAsync(p => p.Username == username, cancellationToken))
            .WithError(ValidationErrors.Player.UsernameExists);
            
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Player.PasswordCannotBeEmpty);
    }
}