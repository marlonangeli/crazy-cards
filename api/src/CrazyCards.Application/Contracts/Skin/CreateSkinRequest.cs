using CrazyCards.Application.Contracts.Images;

namespace CrazyCards.Application.Contracts.Skin;

public class CreateSkinRequest : CreateImageRequest
{
    public string Name { get; set; }
}