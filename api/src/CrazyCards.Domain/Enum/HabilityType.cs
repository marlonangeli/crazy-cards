using CrazyCards.Domain.Enum.Shared;

namespace CrazyCards.Domain.Enum;

public abstract class HabilityType : Enumeration<HabilityType>
{
    public static readonly HabilityType Taunt = new TauntHabilityType();
    public static readonly HabilityType Prepared = new PreparedHabilityType();
    public static readonly HabilityType Stealth = new StealthHabilityType();
    public static readonly HabilityType Shield = new ShieldHabilityType();
    public static readonly HabilityType SpellShield = new SpellShieldHabilityType();
    public static readonly HabilityType Poisonous = new PoisonousHabilityType();
    public static readonly HabilityType Lifesteal = new LifestealHabilityType();
    public static readonly HabilityType Overkill = new OverkillHabilityType();
    public static readonly HabilityType SpellDamage = new SpellDamageHabilityType();
    public static readonly HabilityType FlameOfFire = new FlameOfFireHabilityType();
    public static readonly HabilityType Battlecry = new BattlecryHabilityType();
    public static readonly HabilityType LastBreath = new LastBreathHabilityType();

    protected HabilityType(int value, string name) : base(value, name)
    {
    }
    
    private sealed class TauntHabilityType : HabilityType
    {
        public TauntHabilityType() : base(1, "Provocar")
        {
        }
    }
    
    private sealed class PreparedHabilityType : HabilityType
    {
        public PreparedHabilityType() : base(2, "Preparado")
        {
        }
    }
    
    private sealed class StealthHabilityType : HabilityType
    {
        public StealthHabilityType() : base(3, "Furtividade")
        {
        }
    }
    
    private sealed class ShieldHabilityType : HabilityType
    {
        public ShieldHabilityType() : base(4, "Escudo")
        {
        }
    }
    
    private sealed class SpellShieldHabilityType : HabilityType
    {
        public SpellShieldHabilityType() : base(5, "Escudo de Magia")
        {
        }
    }
    
    private sealed class PoisonousHabilityType : HabilityType
    {
        public PoisonousHabilityType() : base(6, "Venenoso")
        {
        }
    }
    
    private sealed class LifestealHabilityType : HabilityType
    {
        public LifestealHabilityType() : base(7, "Roubo de Vida")
        {
        }
    }
    
    private sealed class OverkillHabilityType : HabilityType
    {
        public OverkillHabilityType() : base(8, "Excesso de Dano")
        {
        }
    }
    
    private sealed class SpellDamageHabilityType : HabilityType
    {
        public SpellDamageHabilityType() : base(9, "Dano de Magia")
        {
        }
    }
    
    private sealed class FlameOfFireHabilityType : HabilityType
    {
        public FlameOfFireHabilityType() : base(10, "Labareda de Fogo")
        {
        }
    }
    
    private sealed class BattlecryHabilityType : HabilityType
    {
        public BattlecryHabilityType() : base(11, "Grito de Guerra")
        {
        }
    }
    
    private sealed class LastBreathHabilityType : HabilityType
    {
        public LastBreathHabilityType() : base(12, "Último Suspiro")
        {
        }
    }
}