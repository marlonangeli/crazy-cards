using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card;

public class MinionCard : Card
{
    public ushort Attack { get; set; }
    public ushort Health { get; set; }
    public override CardType Type => CardType.Minion;
}