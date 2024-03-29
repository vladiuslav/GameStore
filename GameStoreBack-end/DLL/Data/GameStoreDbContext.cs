﻿using DLL.Entities;
using GameStore.DataLogic.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace DLL.Data
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PasswordWithSalt> PasswordWithSalts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Genres)
                .WithMany(g => g.Games);

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Games)
            .WithMany(g => g.Genres);
            modelBuilder.Entity<User>()
                .HasOne(u => u.PasswordWithSalt)
                .WithOne(p => p.User)
                .HasForeignKey<PasswordWithSalt>(p=>p.UserId)
                .IsRequired();
            
        }
        
    }
}
