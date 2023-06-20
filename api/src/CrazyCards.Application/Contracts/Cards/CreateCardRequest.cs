namespace CrazyCards.Application.Contracts.Cards;

public class CreateCardRequest
{
    public ushort ManaCost { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ImageId { get; set; }
    public Guid SkinId { get; set; }
    public Guid ClassId { get; set; }
    public int Rarity { get; set; }
    public int Type { get; set; }
}