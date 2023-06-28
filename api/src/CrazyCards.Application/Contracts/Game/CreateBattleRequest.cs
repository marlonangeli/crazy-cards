namespace CrazyCards.Application.Contracts.Game;

public class CreateBattleRequest
{
    public Guid WaitingRoom1Id { get; set; }
    public Guid WaitingRoom2Id { get; set; }
}