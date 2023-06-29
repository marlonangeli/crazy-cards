using CrazyCards.Application.Contracts.Deck;

namespace CrazyCards.Application.Contracts.Players;

public class PlayerResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public PlayerDeckResponse PlayerDeck { get; set; }
}