namespace CrazyCards.Application.Contracts.Heroes;

public class CreateHeroRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ImageId { get; set; }
    public Guid SkinId { get; set; }
    public Guid ClassId { get; set; }
    public Guid WeaponId { get; set; }
}