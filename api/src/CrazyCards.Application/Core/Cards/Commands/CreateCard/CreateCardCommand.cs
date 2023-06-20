using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Domain.Enum;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Cards.Commands.CreateCard;

public record CreateCardCommand(
    ushort ManaCost,
    string Name,
    string Description,
    Guid ImageId,
    Guid SkinId,
    Guid ClassId,
    Rarity? Rarity,
    CardType? Type) : ICommand<Result<CardResponse>>;