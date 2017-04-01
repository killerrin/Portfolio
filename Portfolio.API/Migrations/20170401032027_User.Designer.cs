using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Portfolio.API.Repositories;

namespace Portfolio.API.Migrations
{
    [DbContext(typeof(PortfolioContext))]
    [Migration("20170401032027_User")]
    partial class User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portfolio.API.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Portfolio.API.Models.Framework", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Frameworks");
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemCategory", b =>
                {
                    b.Property<int>("PortfolioItemID");

                    b.Property<int>("CategoryID");

                    b.HasKey("PortfolioItemID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("PortfolioItemCategory");
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemFramework", b =>
                {
                    b.Property<int>("PortfolioItemID");

                    b.Property<int>("FrameworkID");

                    b.HasKey("PortfolioItemID", "FrameworkID");

                    b.HasIndex("FrameworkID");

                    b.ToTable("PortfolioItemFramework");
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemKeyword", b =>
                {
                    b.Property<int>("PortfolioItemID");

                    b.Property<int>("KeywordID");

                    b.HasKey("PortfolioItemID", "KeywordID");

                    b.HasIndex("KeywordID");

                    b.ToTable("PortfolioItemKeyword");
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemProgLanguage", b =>
                {
                    b.Property<int>("PortfolioItemID");

                    b.Property<int>("ProgrammingLanguageID");

                    b.HasKey("PortfolioItemID", "ProgrammingLanguageID");

                    b.HasIndex("ProgrammingLanguageID");

                    b.ToTable("PortfolioItemProgLanguage");
                });

            modelBuilder.Entity("Portfolio.API.Models.Keyword", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("Portfolio.API.Models.PortfolioItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<bool>("Published");

                    b.Property<string>("SourceCodeUrl");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("PortfolioItems");
                });

            modelBuilder.Entity("Portfolio.API.Models.PortfolioItemLink", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("PortfolioItemID");

                    b.Property<string>("Url");

                    b.HasKey("ID");

                    b.HasIndex("PortfolioItemID");

                    b.ToTable("PortfolioItemLinks");
                });

            modelBuilder.Entity("Portfolio.API.Models.ProgrammingLanguage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ProgrammingLanguages");
                });

            modelBuilder.Entity("Portfolio.API.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Auth_Token");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("Expiry");

                    b.Property<string>("Password_Hash");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemCategory", b =>
                {
                    b.HasOne("Portfolio.API.Models.Category", "Category")
                        .WithMany("Portfolios")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("Categories")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemFramework", b =>
                {
                    b.HasOne("Portfolio.API.Models.Framework", "Framework")
                        .WithMany("Portfolios")
                        .HasForeignKey("FrameworkID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("Frameworks")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemKeyword", b =>
                {
                    b.HasOne("Portfolio.API.Models.Keyword", "Keyword")
                        .WithMany("Portfolios")
                        .HasForeignKey("KeywordID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("Keywords")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemProgLanguage", b =>
                {
                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("ProgrammingLanguages")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Portfolio.API.Models.ProgrammingLanguage", "ProgrammingLanguage")
                        .WithMany("Portfolios")
                        .HasForeignKey("ProgrammingLanguageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portfolio.API.Models.PortfolioItemLink", b =>
                {
                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("Links")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
