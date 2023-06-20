using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Contracts.Common;

namespace CrazyCards.Application.Core.Cards.Queries;

public record GetCardsPaginatedQuery(
        int Page,
        int PageSize)
    : IQuery<PagedList<CardResponse>>;