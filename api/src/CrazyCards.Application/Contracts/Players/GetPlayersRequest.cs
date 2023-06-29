using CrazyCards.Application.Contracts.Common;

namespace CrazyCards.Application.Contracts.Players;

public class GetPlayersRequest : GetPaginatedRequest
{
    public string? Username { get; set; }
    public string? Email { get; set; }
}