using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class GameCard : Entity
{
    public Card.Card OriginalCard { get; set; } = null!;
    public Guid OriginalCardId { get; set; }

    public GameDeck GameDeck { get; set; } = null!;
    public Guid GameDeckId { get; set; }
    public GameCardAttributeProperty Current { get; set; } = new();
    public GameCardStatusProperty Is { get; set; } = new();
    public GameCardPositionProperty At { get; set; } = new();
}