using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Domain.Entities.Card;

public class Skin : Image
{
    public string Name { get; set; } = string.Empty;
}