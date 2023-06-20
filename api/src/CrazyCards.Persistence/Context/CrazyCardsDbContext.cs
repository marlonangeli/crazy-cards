using System.Reflection;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Card.Hability;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Entities.Game;
using CrazyCards.Domain.Entities.Player;
using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CrazyCards.Persistence.Context;

public sealed class CrazyCardsDbContext : DbContext, IDbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Hability> Habilities { get; set; }
    public DbSet<Hero> Heroes { get; set; }
    public DbSet<BattleDeck> BattleDecks { get; set; }
    public DbSet<PlayerDeck> PlayerDecks { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<GameDeck> GameDecks { get; set; }
    public DbSet<GameCard> GameCards { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<Movement> Movements { get; set; }

    public CrazyCardsDbContext(DbContextOptions<CrazyCardsDbContext> options) : base(options)
    {
    }
    
    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : Entity =>
        base.Set<TEntity>();
    
    /// <inheritdoc />
    public async Task<TEntity?> GetBydIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : Entity
    {
        return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public void Insert<TEntity>(TEntity entity)
        where TEntity : Entity =>
        Set<TEntity>().Add(entity);

    /// <inheritdoc />
    public new void Remove<TEntity>(TEntity entity)
        where TEntity : Entity =>
        Set<TEntity>().Remove(entity);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.ApplyUtcDateTimeConverter();

        modelBuilder.Ignore<Entity>();

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void UpdateAuditableEntities()
    {
        var utcNow = DateTime.Now;
        
        foreach (EntityEntry<IEntity> entityEntry in ChangeTracker.Entries<IEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IEntity.CreatedAt)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IEntity.UpdatedAt)).CurrentValue = utcNow;
            }
        }
    }
}