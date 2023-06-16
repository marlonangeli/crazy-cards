using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class GameDeck : Entity
{
    public GameDeck()
    {
        CardsOnHand = new HashSet<Card.Card>();
        CardsOnDeck = new HashSet<Card.Card>();
        CardsOnGraveyard = new HashSet<Card.Card>();
        CardsOnTable = new HashSet<Card.Card>();
    }
    
    public Battle Battle { get; set; } = null!;
    public Guid BattleId { get; set; }
    public ICollection<Card.Card> CardsOnHand { get; set; }
    public ICollection<Card.Card> CardsOnDeck { get; set; }
    public ICollection<Card.Card> CardsOnGraveyard { get; set; }
    public ICollection<Card.Card> CardsOnTable { get; set; }
}