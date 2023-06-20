using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card;

public class SpellCard : Card
{
    public ushort Damage { get; set; }
    public ushort Heal { get; set; }
    public override CardType Type => CardType.Spell;
}