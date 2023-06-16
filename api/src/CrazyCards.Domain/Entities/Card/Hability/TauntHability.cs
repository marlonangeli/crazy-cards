using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public class TauntHability : Hability
{
    protected override string Name => Type.Name;
    protected override string Description => "Oponente deve atacar este personagem";
    public override HabilityType Type => HabilityType.Taunt;
}