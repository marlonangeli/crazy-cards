using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Heroes.Commands;

public record CreateHeroCommand(
    string Name,
    string Description,
    Guid ImageId,
    Guid SkinId,
    Guid ClassId,
    Guid WeaponId) : ICommand<Result<HeroResponse>>;