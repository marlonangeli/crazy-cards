namespace CrazyCards.Domain.Entities.Shared;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;
}