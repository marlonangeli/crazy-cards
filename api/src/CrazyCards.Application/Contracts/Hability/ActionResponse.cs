using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Domain.Enum;

namespace CrazyCards.Application.Contracts.Hability;

public class ActionResponse
{
    public string Description { get; set; }
    public CardResponse? InvokeCard { get; set; }
    public CardType? InvokeCardType { get; set; }
    public int? Damage { get; set; }
    public int? Heal { get; set; }
    public int? Shield { get; set; }
    public bool DamageToAll { get; set; } = false;
    public bool HealToAll { get; set; } = false;
    public bool ShieldToAll { get; set; } = false;
}