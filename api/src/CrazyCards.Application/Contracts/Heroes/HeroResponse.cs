using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Contracts.Skin;

namespace CrazyCards.Application.Contracts.Heroes;

public class HeroResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ImageResponse Image { get; set; }
    public SkinResponse Skin { get; set; }
    public ClassResponse Class { get; set; }
    public CardResponse Weapon { get; set; }
}