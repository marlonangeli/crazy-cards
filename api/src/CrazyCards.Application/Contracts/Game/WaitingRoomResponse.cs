using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Application.Contracts.Players;

namespace CrazyCards.Application.Contracts.Game;

public class WaitingRoomResponse
{
    public Guid Id { get; set; }
    public PlayerResponse Player { get; set; }
    public BattleDeckResponse BattleDeck { get; set; }
    public bool IsWaiting { get; set; }
    public BattleResponse? Battle { get; set; }
}