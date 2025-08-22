using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "JUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 19, 20, 48, 45, 62, DateTimeKind.Local).AddTicks(8430), "admin@bulky.com", true, "$2a$11$qhCKbGPsgX1zEVsd5h0zm.9poThl3rNfYnA0OgqcxUCPplaDuMjOG", "Admin", "admin" },
                    { 2, new DateTime(2025, 8, 19, 20, 48, 45, 189, DateTimeKind.Local).AddTicks(6891), "user@bulky.com", true, "$2a$11$880alQxKXfp28qzhGT5UxuD.lzXQkePMhe3YZ2jt6oTHZQI9I.RSC", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JUsers");
        }
    }
}
