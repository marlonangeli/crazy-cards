namespace CrazyCards.Application.Contracts.Common;

public class GetPaginatedRequest
{
    public uint Page { get; set; } = 1;
    public uint PageSize { get; set; } = 10;
}