using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Heroes;

namespace CrazyCards.Application.Core.Heroes.Queries;

public record GetHeroesPaginatedQuery(int Page, int PageSize, string? Name) : IQuery<PagedList<HeroResponse>>;