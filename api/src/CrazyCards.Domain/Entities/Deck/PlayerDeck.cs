using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Deck;

public class PlayerDeck : Entity
{
    public PlayerDeck()
    {
        BattleDecks = new HashSet<BattleDeck>();
    }
    
    public Guid PlayerId { get; set; }
    public Player.Player Player { get; set; } = null!;
    public ICollection<BattleDeck> BattleDecks { get; set; }
}