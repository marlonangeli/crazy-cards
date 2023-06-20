using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Card;

public class Hero : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
    public Guid SkinId { get; set; }
    public Skin Skin { get; set; } = null!;
    public Guid ClassId { get; set; }
    public Class Class { get; set; } = null!;
    public WeaponCard Weapon { get; set; } = null!;
    public Guid WeaponId { get; set; }
}