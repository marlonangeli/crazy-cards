using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class Round : Entity
{
    public Round()
    {
        Movements = new HashSet<Movement>();
    }
    
    public int Number { get; set; }
    public Battle Battle { get; set; } = null!;
    public Guid BattleId { get; set; }
    public ICollection<Movement> Movements { get; set; }
}