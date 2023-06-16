using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Card.Hability;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Entities.Game;
using CrazyCards.Domain.Entities.Player;
using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Persistence.EntityConfiguration.CrazyCards;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Persistence.Context;

public class CrazyCardsDbContext : DbContext
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

    public CrazyCardsDbContext(DbContextOptions<CrazyCardsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ClassEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new HabilityEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new HeroEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BattleDeckEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerDeckEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerEntityTypeConfiguration());

        modelBuilder.Ignore<Battle>();
        modelBuilder.Ignore<GameDeck>();
        modelBuilder.Ignore<Round>();
        modelBuilder.Ignore<Entity>();
        modelBuilder.Ignore<Movement<Card, Card>>();

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdatedAt();
        
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void UpdatedAt()
    {
        var entities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified)
            .Select(e => e.Entity as Entity)
            .ToList();

        foreach (var entity in entities)
        {
            entity?.SetUpdatedAt();
        }
    }
}