﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Portfolio.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Frameworks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frameworks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItemCategory",
                columns: table => new
                {
                    PortfolioItemID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItemCategory", x => new { x.PortfolioItemID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_PortfolioItemCategory_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioItemCategory_PortfolioItems_PortfolioItemID",
                        column: x => x.PortfolioItemID,
                        principalTable: "PortfolioItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItemFramework",
                columns: table => new
                {
                    PortfolioItemID = table.Column<int>(nullable: false),
                    FrameworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItemFramework", x => new { x.PortfolioItemID, x.FrameworkID });
                    table.ForeignKey(
                        name: "FK_PortfolioItemFramework_Frameworks_FrameworkID",
                        column: x => x.FrameworkID,
                        principalTable: "Frameworks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioItemFramework_PortfolioItems_PortfolioItemID",
                        column: x => x.PortfolioItemID,
                        principalTable: "PortfolioItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItemKeyword",
                columns: table => new
                {
                    PortfolioItemID = table.Column<int>(nullable: false),
                    KeywordID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItemKeyword", x => new { x.PortfolioItemID, x.KeywordID });
                    table.ForeignKey(
                        name: "FK_PortfolioItemKeyword_Keywords_KeywordID",
                        column: x => x.KeywordID,
                        principalTable: "Keywords",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioItemKeyword_PortfolioItems_PortfolioItemID",
                        column: x => x.PortfolioItemID,
                        principalTable: "PortfolioItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItemProgLanguage",
                columns: table => new
                {
                    PortfolioItemID = table.Column<int>(nullable: false),
                    ProgrammingLanguageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItemProgLanguage", x => new { x.PortfolioItemID, x.ProgrammingLanguageID });
                    table.ForeignKey(
                        name: "FK_PortfolioItemProgLanguage_PortfolioItems_PortfolioItemID",
                        column: x => x.PortfolioItemID,
                        principalTable: "PortfolioItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioItemProgLanguage_ProgrammingLanguages_ProgrammingLanguageID",
                        column: x => x.ProgrammingLanguageID,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItemCategory_CategoryID",
                table: "PortfolioItemCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItemFramework_FrameworkID",
                table: "PortfolioItemFramework",
                column: "FrameworkID");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItemKeyword_KeywordID",
                table: "PortfolioItemKeyword",
                column: "KeywordID");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItemProgLanguage_ProgrammingLanguageID",
                table: "PortfolioItemProgLanguage",
                column: "ProgrammingLanguageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioItemCategory");

            migrationBuilder.DropTable(
                name: "PortfolioItemFramework");

            migrationBuilder.DropTable(
                name: "PortfolioItemKeyword");

            migrationBuilder.DropTable(
                name: "PortfolioItemProgLanguage");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Frameworks");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "PortfolioItems");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");
        }
    }
}
