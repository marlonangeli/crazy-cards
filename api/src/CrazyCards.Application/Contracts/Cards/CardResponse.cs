using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Domain.Entities.Card.Hability;
using CrazyCards.Domain.Enum;

namespace CrazyCards.Application.Contracts.Cards;

public class CardResponse
{
    public Guid Id { get; set; }
    public ushort ManaCost { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ImageResponse Image { get; set; }
    public SkinResponse Skin { get; set; }
    public ClassResponse Class { get; set; }
    public Rarity Rarity { get; set; }
    public CardType Type { get; set; }
    public IEnumerable<HabilityResponse>? Habilities { get; set; }
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}