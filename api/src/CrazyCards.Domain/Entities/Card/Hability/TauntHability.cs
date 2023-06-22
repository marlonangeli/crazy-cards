using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public class TauntHability : Hability
{
    public static string Name => HabilityType.Taunt.Name;
    public static string Description => "Oponente deve atacar este personagem";
    public override HabilityType Type => HabilityType.Taunt;
}