using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Persistence.Context;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }
}