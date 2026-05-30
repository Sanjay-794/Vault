using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Devnet.Vault.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ItemAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserSecretKey",
                table: "UserDetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserSecretKey",
                table: "UserDetails");
        }
    }
}
