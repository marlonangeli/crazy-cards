using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public class Action
{
    public string Description { get; set; }
    public Guid? InvokeCardId { get; set; }
    public Card? InvokeCard { get; set; }
    public CardType? InvokeCardType { get; set; }
    public ushort? Damage { get; set; }
    public ushort? Heal { get; set; }
    public ushort? Shield { get; set; }
    public bool DamageToAll { get; set; } = false;
    public bool HealToAll { get; set; } = false;
    public bool ShieldToAll { get; set; } = false;
}