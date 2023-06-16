using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card;

public class WeaponCard : Card
{
    public ushort Damage { get; set; }
    public ushort Durability { get; set; }
    public ushort Shield { get; set; }
    public override CardType Type => CardType.Weapon;
}