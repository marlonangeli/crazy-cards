namespace CrazyCards.Application.Contracts.Hability;

public class CreateHabilityRequest
{
    public Guid CardId { get; set; }
    public CreateActionRequest? Action { get; set; }
    public int Type { get; set; }
}