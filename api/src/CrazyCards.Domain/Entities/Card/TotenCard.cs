using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card;

public class TotenCard : Card
{
    public ushort Heal { get; set; }
    public ushort Shield { get; set; }
    public override CardType Type => CardType.Toten;
}