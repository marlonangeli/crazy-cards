using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Card;

public class Class : Entity
{
    public Class()
    {
        Cards = new HashSet<Card>();
    }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
    public Guid SkinId { get; set; }
    public Skin Skin { get; set; } = null!;
    public ICollection<Card> Cards { get; set; }
}