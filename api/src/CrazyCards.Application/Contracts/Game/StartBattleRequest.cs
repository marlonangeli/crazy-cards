namespace CrazyCards.Application.Contracts.Game;

public class StartBattleRequest
{
    public Guid PlayerId { get; set; }
    public Guid BattleDeckId { get; set; }
}