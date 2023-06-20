using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public class LastBreathHability : Hability
{
    protected override string Name => Type.Name; 
    protected override string Description => "Faz uma ação quando morre";
    public override HabilityType Type => HabilityType.LastBreath;
}