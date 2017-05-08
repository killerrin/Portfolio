using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.API.Migrations
{
    public partial class ExpandedPortfolioItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkType",
                table: "PortfolioItemLinks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Awards",
                table: "PortfolioItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PortfolioItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "PortfolioItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyRole",
                table: "PortfolioItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkType",
                table: "PortfolioItemLinks");

            migrationBuilder.DropColumn(
                name: "Awards",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "MyRole",
                table: "PortfolioItems");
        }
    }
}
