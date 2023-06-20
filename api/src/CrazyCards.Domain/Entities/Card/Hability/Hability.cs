using System.ComponentModel.DataAnnotations.Schema;
using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Domain.Enum;

namespace CrazyCards.Domain.Entities.Card.Hability;

public abstract class Hability : Entity
{
    [NotMapped]
    protected abstract string Name { get; }
    [NotMapped]
    protected abstract string Description { get; }
    public Card Card { get; set; } = null!;
    public Guid CardId { get; set; }
    public Action? Action { get; set; }
    public virtual HabilityType Type { get; set; }
}