using CrazyCards.Domain.Enum.Shared;

namespace CrazyCards.Domain.Enum;

public abstract class CardType : Enumeration<CardType>
{
    public static readonly CardType Minion = new MinionCardType();
    public static readonly CardType Spell = new SpellCardType();
    public static readonly CardType Weapon = new WeaponCardType();
    public static readonly CardType Toten = new TotenCardType();

    protected CardType(int value, string name) : base(value, name)
    {
    }

    private sealed class MinionCardType : CardType
    {
        public MinionCardType() : base(1, "Lacaio")
        {
        }
    }

    private sealed class SpellCardType : CardType
    {
        public SpellCardType() : base(2, "Feitiço")
        {
        }
    }

    private sealed class WeaponCardType : CardType
    {
        public WeaponCardType() : base(3, "Arma")
        {
        }
    }
    
    private sealed class TotenCardType : CardType
    {
        public TotenCardType() : base(4, "Totem")
        {
        }
    }
}