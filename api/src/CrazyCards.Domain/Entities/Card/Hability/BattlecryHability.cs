using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public class BattlecryHability : Hability
{
    protected override string Name => Type.Name;
    protected override string Description => "Faz uma ação quando entra em campo";
    public override HabilityType Type => HabilityType.Battlecry;
}