using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card;

public abstract class Card : Entity
{
    public Card()
    {
        Habilities = new HashSet<Hability.Hability>();
    }

    public ushort ManaCost { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
    public Guid SkinId { get; set; }
    public Skin Skin { get; set; } = null!;
    public Guid ClassId { get; set; }
    public Class Class { get; set; } = null!;
    public Rarity Rarity { get; set; } = null!;
    public virtual CardType Type { get; set; } = null!;
    public ICollection<Hability.Hability> Habilities { get; set; }
}