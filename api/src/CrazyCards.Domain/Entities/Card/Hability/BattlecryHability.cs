using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public class BattlecryHability : Hability
{
    public static string Name => HabilityType.Battlecry.Name;
    public static string Description => "Faz uma ação quando entra em campo";
    public override HabilityType Type => HabilityType.Battlecry;
}