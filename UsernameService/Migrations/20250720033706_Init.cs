using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsernameService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsernameRecords",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsernameRecords", x => x.AccountId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsernameRecords_Username",
                table: "UsernameRecords",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsernameRecords");
        }
    }
}
