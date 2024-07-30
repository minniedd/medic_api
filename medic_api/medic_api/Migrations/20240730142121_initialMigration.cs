using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medic_api.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orders = table.Column<int>(type: "int", nullable: true),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    refreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    isBlocked = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
