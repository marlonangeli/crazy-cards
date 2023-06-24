using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Contracts.Heroes;

namespace CrazyCards.Application.Contracts.Deck;

public class BattleDeckResponse
{
    public Guid Id { get; set; }
    public bool IsDefault { get; set; }
    public PlayerDeckResponse PlayerDeck { get; set; }
    public HeroResponse Hero { get; set; }
    public ICollection<CardResponse> Cards { get; set; }
}