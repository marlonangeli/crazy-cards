using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Application.Contracts.Classes;

public class ClassResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Image Image { get; set; }
    public Domain.Entities.Card.Skin Skin { get; set; }
}