using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class GameDeck : Entity
{
    public GameDeck()
    {
        Cards = new HashSet<GameCard>();
    }
    
    public Battle Battle { get; set; } = null!;
    public Guid BattleId { get; set; }
    public ICollection<GameCard> Cards { get; set; }
}