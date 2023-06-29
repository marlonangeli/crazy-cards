using CrazyCards.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Interfaces;

public interface IDbContext
{
    /// <summary>
    /// Gets the database set for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <returns>The database set for the specified entity type.</returns>
    DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity;

    /// <summary>
    /// Gets the entity with the specified identifier.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="id">The entity identifier.</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The maybe instance that may contain the <typeparamref name="TEntity"/> with the specified identifier.</returns>
    Task<TEntity?> GetBydIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : Entity;

    /// <summary>
    /// Inserts the specified entity into the database.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity to be inserted into the database.</param>
    void Insert<TEntity>(TEntity entity)
        where TEntity : Entity;

    /// <summary>
    /// Removes the specified entity from the database.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity to be removed from the database.</param>
    void Remove<TEntity>(TEntity entity)
        where TEntity : Entity;

    /// <summary>
    /// Saves all of the pending changes in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities that have been saved.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    
    Task BulkInsertEntitiesAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task BulkSaveAsync(CancellationToken cancellationToken);
}