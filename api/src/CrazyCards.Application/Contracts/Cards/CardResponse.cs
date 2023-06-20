using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Card.Hability;
using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Application.Contracts.Cards;

public class CardResponse
{
    public Guid Id { get; set; }
    public ushort ManaCost { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Image Image { get; set; }
    public Domain.Entities.Card.Skin Skin { get; set; }
    public Class Class { get; set; }
    public int Rarity { get; set; }
    public int Type { get; set; }
    public IEnumerable<Hability>? Habilities { get; set; }
}