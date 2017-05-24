using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Portfolio.API.Migrations
{
    public partial class RelatedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatedItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItemRelatedItem",
                columns: table => new
                {
                    PortfolioItemID = table.Column<int>(nullable: false),
                    RelatedItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItemRelatedItem", x => new { x.PortfolioItemID, x.RelatedItemID });
                    table.ForeignKey(
                        name: "FK_PortfolioItemRelatedItem_PortfolioItems_PortfolioItemID",
                        column: x => x.PortfolioItemID,
                        principalTable: "PortfolioItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioItemRelatedItem_RelatedItems_RelatedItemID",
                        column: x => x.RelatedItemID,
                        principalTable: "RelatedItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItemRelatedItem_RelatedItemID",
                table: "PortfolioItemRelatedItem",
                column: "RelatedItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioItemRelatedItem");

            migrationBuilder.DropTable(
                name: "RelatedItems");
        }
    }
}
