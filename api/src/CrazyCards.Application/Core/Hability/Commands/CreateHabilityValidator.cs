using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Domain.Entities.Card;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Hability.Commands;

internal sealed class CreateHabilityValidator : AbstractValidator<CreateHabilityCommand>
{
    public CreateHabilityValidator(IDbContext dbContext)
    {
        RuleFor(x => x.CardId)
            .MustAsync(async (id, cts) => await dbContext.Set<Card>().AnyAsync(x => x.Id == id, cts))
            .WithError(ValidationErrors.Hability.CardDoesNotExist);

        RuleFor(x => x.Type)
            .NotNull()
            .WithError(ValidationErrors.Hability.TypeDoesNotExist);

        RuleFor(x => x.Action.Description)
            .NotNull()
            .NotEmpty()
            .When(x => x.Action != null)
            .WithError(ValidationErrors.Hability.ActionDescriptionIsNullOrEmpty)
            .MaximumLength(512)
            .WithError(ValidationErrors.Hability.ActionDescriptionIsTooLong);

        RuleFor(x => x.Action.InvokeCardId)
            .MustAsync(async (id, cts) => await dbContext.Set<Card>().AnyAsync(x => x.Id == id, cts))
            .When(x => x.Action is { InvokeCardId: not null })
    .WithError(ValidationErrors.Hability.ActionInvokeCardDoesNotExist);
        
        RuleFor(x => x.Action.InvokeCardType)
            .NotNull()
            .When(x => x.Action is { InvokeCardType: not null })
            .WithError(ValidationErrors.Hability.ActionInvokeCardTypeDoesNotExist);
        
        RuleFor(x => x.Action.Damage)
            .GreaterThanOrEqualTo((ushort)0)
            .When(x => x.Action is { Damage: not null })
            .WithError(ValidationErrors.Hability.ActionDamageIsNegative);

        RuleFor(x => x.Action.Heal)
            .GreaterThanOrEqualTo((ushort)0)
            .When(x => x.Action is { Heal: not null })
            .WithError(ValidationErrors.Hability.ActionHealIsNegative);
        
        RuleFor(x => x.Action.Shield)
            .GreaterThanOrEqualTo((ushort)0)
            .When(x => x.Action is { Shield: not null })
            .WithError(ValidationErrors.Hability.ActionShieldIsNegative);
    }
}