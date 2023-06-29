using CrazyCards.Application.Contracts.Common;

namespace CrazyCards.Application.Contracts.Heroes;

public class GetHeroesRequest : GetPaginatedRequest
{
    public string? Name { get; set; }
}