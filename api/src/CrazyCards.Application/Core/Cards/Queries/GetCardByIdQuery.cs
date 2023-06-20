using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Cards.Queries;

public record GetCardByIdQuery(
        Guid Id)
    : IQuery<Result<CardResponse>>;