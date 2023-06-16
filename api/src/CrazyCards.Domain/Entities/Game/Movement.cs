using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Game;

public class Movement<TInitiator, TTarget> : Entity 
    where TInitiator : Card.Card 
    where TTarget : Card.Card
{
    public TInitiator CardInitiator { get; set; } = null!;
    public Guid CardInitiatorId { get; set; }
    public TTarget CardTarget { get; set; } = null!;
    public Guid CardTargetId { get; set; }
    public Round Round { get; set; } = null!;
    public Guid RoundId { get; set; }
}