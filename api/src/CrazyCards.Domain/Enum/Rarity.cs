﻿using CrazyCards.Domain.Enum.Shared;

namespace CrazyCards.Domain.Enum;

public abstract class Rarity : Enumeration<Rarity>
{
    public static readonly Rarity Common = new CommonRarity();
    public static readonly Rarity Rare = new RareRarity();
    public static readonly Rarity Epic = new EpicRarity();
    public static readonly Rarity Legendary = new LegendaryRarity();

    protected Rarity(int value, string name) : base(value, name)
    {
    }

    public abstract double Percentage { get; }
    
    private sealed class CommonRarity : Rarity
    {
        public CommonRarity() : base(1, "Comum")
        {
        }

        public override double Percentage => 0.7;
    }
    
    private sealed class RareRarity : Rarity
    {
        public RareRarity() : base(2, "Raro")
        {
        }

        public override double Percentage => 0.2;
    }
    
    private sealed class EpicRarity : Rarity
    {
        public EpicRarity() : base(3, "Épico")
        {
        }

        public override double Percentage => 0.1;
    }
    
    private sealed class LegendaryRarity : Rarity
    {
        public LegendaryRarity() : base(4, "Lendário")
        {
        }

        public override double Percentage => 0.01;
    }
}