using CrazyCards.Application.Contracts.Common;

namespace CrazyCards.Application.Contracts.Cards;

public class GetCardsRequest : GetPaginatedRequest
{
    public string? Name { get; set; }
    public int? Type { get; set; }
    public int? Rarity { get; set; }
}