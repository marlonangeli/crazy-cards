namespace CrazyCards.Application.Contracts.Hability;

public class CreateActionRequest
{
    public string Description { get; set; } = null!;
    public Guid? InvokeCardId { get; set; }
    public int? InvokeCardType { get; set; }
    public ushort? Damage { get; set; }
    public ushort? Heal { get; set; }
    public ushort? Shield { get; set; }
    public bool DamageToAll { get; set; } = false;
    public bool HealToAll { get; set; } = false;
    public bool ShieldToAll { get; set; } = false;
}