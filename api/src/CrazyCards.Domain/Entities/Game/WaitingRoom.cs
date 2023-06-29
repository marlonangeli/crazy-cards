using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class WaitingRoom : Entity
{
    public bool IsWaiting { get; set; }
    public Player.Player Player { get; set; }
    public Guid PlayerId { get; set; }
    public BattleDeck BattleDeck { get; set; }
    public Guid BattleDeckId { get; set; }
    public Battle Battle { get; set; }
    public Guid? BattleId { get; set; }
}