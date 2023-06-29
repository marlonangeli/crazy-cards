namespace CrazyCards.Application.Contracts.Deck;

public class CreateBattleDeckRequest
{
    public Guid PlayerDeckId { get; set; }
    public Guid HeroId { get; set; }
    public bool IsDefault { get; set; } = false;
}