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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        // Create the Model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Category Join Table
            modelBuilder.Entity<PortfolioItemCategory>()
                .HasKey(x => new { x.PortfolioItemID, x.CategoryID });

            modelBuilder.Entity<PortfolioItemCategory>()
                .HasOne(p => p.PortfolioItem)
                .WithMany(pi => pi.Categories)
                .HasForeignKey(p => p.PortfolioItemID);

            modelBuilder.Entity<PortfolioItemCategory>()
                .HasOne(p => p.Category)
                .WithMany(f => f.Portfolios)
                .HasForeignKey(p => p.CategoryID);
            #endregion

            #region Framework Join Table
            modelBuilder.Entity<PortfolioItemFramework>()
                .HasKey(x => new { x.PortfolioItemID, x.FrameworkID });

            modelBuilder.Entity<PortfolioItemFramework>()
                .HasOne(p => p.PortfolioItem)
                .WithMany(pi => pi.Frameworks)
                .HasForeignKey(p => p.PortfolioItemID);

            modelBuilder.Entity<PortfolioItemFramework>()
                .HasOne(p => p.Framework)
                .WithMany(f => f.Portfolios)
                .HasForeignKey(p => p.FrameworkID);
            #endregion

            #region Keyword Join Table
            modelBuilder.Entity<PortfolioItemKeyword>()
                .HasKey(x => new { x.PortfolioItemID, x.KeywordID });

            modelBuilder.Entity<PortfolioItemKeyword>()
                .HasOne(p => p.PortfolioItem)
                .WithMany(pi => pi.Keywords)
                .HasForeignKey(p => p.PortfolioItemID);

            modelBuilder.Entity<PortfolioItemKeyword>()
                .HasOne(p => p.Keyword)
                .WithMany(f => f.Portfolios)
                .HasForeignKey(p => p.KeywordID);
            #endregion

            #region Programming Language Join Table
            modelBuilder.Entity<PortfolioItemProgLanguage>()
                .HasKey(x => new { x.PortfolioItemID, x.ProgrammingLanguageID });

            modelBuilder.Entity<PortfolioItemProgLanguage>()
                .HasOne(p => p.PortfolioItem)
                .WithMany(pi => pi.ProgrammingLanguages)
                .HasForeignKey(p => p.PortfolioItemID);

            modelBuilder.Entity<PortfolioItemProgLanguage>()
                .HasOne(p => p.ProgrammingLanguage)
                .WithMany(f => f.Portfolios)
                .HasForeignKey(p => p.ProgrammingLanguageID);
            #endregion
        }
    }
}
