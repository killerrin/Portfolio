using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.API.Migrations
{
    public partial class UpdatedUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Auth_Token",
                table: "Users",
                newName: "AuthToken");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Auth_Token",
                table: "Users",
                newName: "IX_Users_AuthToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthToken",
                table: "Users",
                newName: "Auth_Token");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AuthToken",
                table: "Users",
                newName: "IX_Users_Auth_Token");
        }
    }
}
