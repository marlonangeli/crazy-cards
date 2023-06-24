using CrazyCards.Application.Contracts.Players;

namespace CrazyCards.Application.Contracts.Deck;

public class PlayerDeckResponse
{
    public Guid Id { get; set; }
    public PlayerResponse Player { get; set; }
    public ICollection<BattleDeckResponse> BattleDecks { get; set; }
}