using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Domain.Entities.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Classes.Commands.CreateClass;

public class CreateClassValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassValidator(IDbContext dbContext)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Class.NameIsEmptyOrNull)
            .MaximumLength(50)
            .WithError(ValidationErrors.Class.NameIsTooLong);
        
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Class.DescriptionIsEmptyOrNull)
            .MaximumLength(512)
            .WithError(ValidationErrors.Class.DescriptionIsTooLong);

        RuleFor(x => x.ImageId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<Image>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Class.ImageDoesNotExist);

        RuleFor(x => x.SkinId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<Domain.Entities.Card.Skin>().AnyAsync(c => c.Id == id, cts))
            .WithError(ValidationErrors.Class.SkinDoesNotExist);
    }
}