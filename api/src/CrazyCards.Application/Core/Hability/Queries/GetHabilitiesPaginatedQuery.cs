using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Hability;

namespace CrazyCards.Application.Core.Hability.Queries;

public record GetHabilitiesPaginatedQuery(
    int Page, 
    int PageSize) : IQuery<PagedList<HabilityResponse>>;