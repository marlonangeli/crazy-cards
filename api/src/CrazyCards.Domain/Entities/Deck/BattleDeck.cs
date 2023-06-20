using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Deck;

public class BattleDeck : Entity
{
    public BattleDeck()
    {
        Cards = new HashSet<Card.Card>();
    }
    
    public bool IsDefault { get; set; }
    public Guid PlayerDeckId { get; set; }
    public PlayerDeck PlayerDeck { get; set; } = null!;
    public Guid HeroId { get; set; }
    public Card.Hero Hero { get; set; } = null!;
    public ICollection<Card.Card> Cards { get; set; }
}