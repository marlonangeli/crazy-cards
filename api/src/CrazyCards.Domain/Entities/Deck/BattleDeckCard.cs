namespace CrazyCards.Domain.Entities.Deck;

public class BattleDeckCard
{
    public Guid DeckId { get; set; }
    public BattleDeck Deck { get; set; }
    public Guid CardId { get; set; }
    public Card.Card Card { get; set; }
}