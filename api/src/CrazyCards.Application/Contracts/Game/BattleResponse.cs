using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Application.Contracts.Players;

namespace CrazyCards.Application.Contracts.Game;

public class BattleResponse
{
    public Guid Id { get; set; }
    public Guid WinnerId { get; set; }
    public Guid LoserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public PlayerResponse Player1 { get; set; }
    public PlayerResponse Player2 { get; set; }
    public BattleDeckResponse Player1Deck { get; set; }
    public BattleDeckResponse Player2Deck { get; set; }
    public ICollection<RoundResponse> Rounds { get; set; }
}