using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Contracts.Skin;

namespace CrazyCards.Application.Contracts.Classes;

public class ClassResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ImageResponse Image { get; set; }
    public SkinResponse Skin { get; set; }
}