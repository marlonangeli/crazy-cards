namespace CrazyCards.Application.Contracts.Classes;

public class CreateClassRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ImageId { get; set; }
    public Guid SkinId { get; set; }
}