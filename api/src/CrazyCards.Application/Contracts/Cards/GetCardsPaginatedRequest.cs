namespace CrazyCards.Application.Contracts.Cards;

public class GetCardsPaginatedRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}