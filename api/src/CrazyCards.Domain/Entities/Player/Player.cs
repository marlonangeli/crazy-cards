using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Player;

public class Player : Entity
{
    public bool? IsActive { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PlayerDeck PlayerDeck { get; set; } = null!;
    public Guid PlayerDeckId { get; set; }
}