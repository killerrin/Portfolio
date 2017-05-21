using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models;
using Portfolio.API.Models.JoinTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options)
            : base(options)
        {

        }

        // Create the DbSets
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagType> TagTypes { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<PortfolioItemLink> PortfolioItemLinks { get; set; }
        public DbSet<User> Users { get; set; }

        // Create the Model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tag Join Table
            modelBuilder.Entity<PortfolioItemTag>()
                .HasKey(x => new { x.PortfolioItemID, x.TagID });

            modelBuilder.Entity<PortfolioItemTag>()
                .HasOne(p => p.PortfolioItem)
                .WithMany(pi => pi.Tags)
                .HasForeignKey(p => p.PortfolioItemID);

            modelBuilder.Entity<PortfolioItemTag>()
                .HasOne(p => p.Tag)
                .WithMany(f => f.Portfolios)
                .HasForeignKey(p => p.TagID);
            #endregion

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.AuthToken);
        }
    }
}
