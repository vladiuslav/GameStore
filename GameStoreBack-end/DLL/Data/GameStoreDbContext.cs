using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Data
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Ganre> Ganres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Ganres)
                .WithMany(g => g.Games);
            modelBuilder.Entity<Ganre>()
                .HasMany(g => g.Games)
                .WithMany(g => g.Ganres);
        }
    }
}
