using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Portfolio.API.Repositories;
using Portfolio.API.Models.Enums;

namespace Portfolio.API.Migrations
{
    [DbContext(typeof(PortfolioContext))]
    [Migration("20170531181153_PortfolioItem_CoverImageUrl")]
    partial class PortfolioItem_CoverImageUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemRelatedItem", b =>
                {
                    b.Property<int>("PortfolioItemID");

                    b.Property<int>("RelatedItemID");

                    b.HasKey("PortfolioItemID", "RelatedItemID");

                    b.HasIndex("RelatedItemID");

                    b.ToTable("PortfolioItemRelatedItem");
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemTag", b =>
                {
                    b.Property<int>("PortfolioItemID");

                    b.Property<int>("TagID");

                    b.HasKey("PortfolioItemID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("PortfolioItemTag");
                });

            modelBuilder.Entity("Portfolio.API.Models.PortfolioItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Awards");

                    b.Property<string>("CoverImageUrl");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Features");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("MyRole");

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

                    b.Property<int>("LinkType");

                    b.Property<string>("Name");

                    b.Property<int>("PortfolioItemID");

                    b.Property<string>("Url");

                    b.HasKey("ID");

                    b.HasIndex("PortfolioItemID");

                    b.ToTable("PortfolioItemLinks");
                });

            modelBuilder.Entity("Portfolio.API.Models.RelatedItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("RelatedItems");
                });

            modelBuilder.Entity("Portfolio.API.Models.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("TagTypeID");

                    b.HasKey("ID");

                    b.HasIndex("TagTypeID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Portfolio.API.Models.TagType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("TagTypes");
                });

            modelBuilder.Entity("Portfolio.API.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthToken");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("Expiry");

                    b.Property<string>("Password_Hash");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.HasIndex("AuthToken");

                    b.HasIndex("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemRelatedItem", b =>
                {
                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("RelatedItems")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Portfolio.API.Models.RelatedItem", "RelatedItem")
                        .WithMany("Portfolios")
                        .HasForeignKey("RelatedItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portfolio.API.Models.JoinTables.PortfolioItemTag", b =>
                {
                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("Tags")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Portfolio.API.Models.Tag", "Tag")
                        .WithMany("Portfolios")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portfolio.API.Models.PortfolioItemLink", b =>
                {
                    b.HasOne("Portfolio.API.Models.PortfolioItem", "PortfolioItem")
                        .WithMany("Links")
                        .HasForeignKey("PortfolioItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Portfolio.API.Models.Tag", b =>
                {
                    b.HasOne("Portfolio.API.Models.TagType", "TagType")
                        .WithMany()
                        .HasForeignKey("TagTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
