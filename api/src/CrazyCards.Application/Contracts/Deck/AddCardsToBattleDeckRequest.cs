namespace CrazyCards.Application.Contracts.Deck;

public class AddCardsToBattleDeckRequest
{
    public Guid BattleDeckId { get; set; }
    public ICollection<Guid> CardIds { get; set; }
}