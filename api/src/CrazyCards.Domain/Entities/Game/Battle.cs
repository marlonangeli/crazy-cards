using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class Battle : Entity
{
    public Battle()
    {
        Rounds = new HashSet<Round>();
    }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan TotalTime => EndTime - StartTime;
    
    public Player.Player Player1 { get; set; }
    public Guid Player1Id { get; set; }
    public BattleDeck Player1Deck { get; set; }
    public Guid Player1DeckId { get; set; }
    
    public Player.Player Player2 { get; set; }
    public Guid Player2Id { get; set; }
    public BattleDeck Player2Deck { get; set; }
    public Guid Player2DeckId { get; set; }
    
    public Player.Player Winner { get; set; }
    public Player.Player Loser { get; set; }
    public ICollection<Round> Rounds { get; set; }
}