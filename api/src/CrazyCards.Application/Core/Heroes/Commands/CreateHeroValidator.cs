using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Heroes.Commands;

internal sealed class CreateHeroValidator : AbstractValidator<CreateHeroCommand>
{
    public CreateHeroValidator(IDbContext dbContext)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Hero.NameCannotBeNullOrEmpty)
            .MaximumLength(50)
            .WithError(ValidationErrors.Hero.NameCannotBeLongerThan50Characters);
        
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Hero.DescriptionCannotBeNullOrEmpty)
            .MaximumLength(512)
            .WithError(ValidationErrors.Hero.DescriptionCannotBeLongerThan512Characters);
        
        RuleFor(x => x.ImageId)
            .MustAsync(async (id, cts) => 
                await dbContext.Set<Image>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Hero.ImageDoesNotExist);

        RuleFor(x => x.SkinId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<Domain.Entities.Card.Skin>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Hero.SkinDoesNotExist);
        
        RuleFor(x => x.ClassId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<Class>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Hero.ClassDoesNotExist);
        
        RuleFor(x => x.WeaponId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<WeaponCard>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Hero.WeaponDoesNotExist);
    }
}