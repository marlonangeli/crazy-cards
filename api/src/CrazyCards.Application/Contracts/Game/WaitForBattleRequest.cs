namespace CrazyCards.Application.Contracts.Game;

public class WaitForBattleRequest
{
    public Guid PlayerId { get; set; }
    public Guid BattleDeckId { get; set; }
}