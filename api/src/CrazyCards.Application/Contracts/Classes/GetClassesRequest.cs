using CrazyCards.Application.Contracts.Common;

namespace CrazyCards.Application.Contracts.Classes;

public class GetClassesRequest : GetPaginatedRequest
{
    public string? Name { get; set; }
}