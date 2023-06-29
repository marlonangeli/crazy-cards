using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Domain.Enum;

namespace CrazyCards.Application.Contracts.Hability;

public class HabilityResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CardResponse Card { get; set; } = null!;
    public ActionResponse? Action { get; set; }
    public HabilityType Type { get; set; }
}