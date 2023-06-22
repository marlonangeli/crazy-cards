using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Heroes.Queries;

public record GetHeroByIdQuery(Guid Id) : IQuery<Result<HeroResponse>>;
