using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public class LastBreathHability : Hability
{
    public static string Name => HabilityType.LastBreath.Name; 
    public static string Description => "Faz uma ação quando morre";
    public override HabilityType Type => HabilityType.LastBreath;
}