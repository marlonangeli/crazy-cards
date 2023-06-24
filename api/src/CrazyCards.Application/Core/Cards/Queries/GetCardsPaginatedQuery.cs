using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Domain.Enum;

namespace CrazyCards.Application.Core.Cards.Queries;

public record GetCardsPaginatedQuery(
        int Page,
        int PageSize,
        string? Name,
        CardType? Type,
        Rarity? Rarity)
    : IQuery<PagedList<CardResponse>>;