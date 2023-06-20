namespace CrazyCards.Domain.Entities.Shared;

public interface IEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
}