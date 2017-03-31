using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Portfolio.API.Migrations
{
    public partial class PortfolioItemBasicData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PortfolioItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "PortfolioItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "PortfolioItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SourceCodeUrl",
                table: "PortfolioItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PortfolioItemLinks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PortfolioItemID = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItemLinks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PortfolioItemLinks_PortfolioItems_PortfolioItemID",
                        column: x => x.PortfolioItemID,
                        principalTable: "PortfolioItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItemLinks_PortfolioItemID",
                table: "PortfolioItemLinks",
                column: "PortfolioItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioItemLinks");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "SourceCodeUrl",
                table: "PortfolioItems");
        }
    }
}
