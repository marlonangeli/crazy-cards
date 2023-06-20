using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Cards.Commands.CreateCard;

public sealed class CreateCardValidator : AbstractValidator<CreateCardCommand>
{
    public CreateCardValidator(IDbContext dbContext)
    {
        RuleFor(x => x.ManaCost)
            .GreaterThanOrEqualTo((ushort)0)
            .WithError(ValidationErrors.Card.ManaCostIsNegative);
        
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Card.NameIsNullOrEmpty)
            .MaximumLength(100)
            .WithError(ValidationErrors.Card.NameIsTooLong);

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Card.DescriptionIsNullOrEmpty)
            .MaximumLength(512)
            .WithError(ValidationErrors.Card.DescriptionIsTooLong);

        RuleFor(x => x.ClassId)
            .MustAsync(async (id, cts) => 
                await dbContext.Set<Class>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Card.ClassDoesNotExist);
        
        RuleFor(x => x.ImageId)
            .MustAsync(async (id, cts) => 
                await dbContext.Set<Image>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Card.ImageDoesNotExist);
        
        RuleFor(x => x.SkinId)
            .MustAsync(async (id, cts) => 
                await dbContext.Set<Domain.Entities.Card.Skin>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Card.SkinDoesNotExist);
        
        RuleFor(x => x.Rarity)
            .NotNull()
            .WithError(ValidationErrors.Card.RarityDoesNotExist);
        
        RuleFor(x => x.Type)
            .NotNull()
            .WithError(ValidationErrors.Card.TypeDoesNotExist);
    }
}