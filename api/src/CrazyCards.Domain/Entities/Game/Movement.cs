using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class Movement : Entity
{
    public GameCard CardInitiator { get; set; } = null!;
    public Guid CardInitiatorId { get; set; }
    public GameCard CardTarget { get; set; } = null!;
    public Guid CardTargetId { get; set; }
    public Round Round { get; set; } = null!;
    public Guid RoundId { get; set; }
}