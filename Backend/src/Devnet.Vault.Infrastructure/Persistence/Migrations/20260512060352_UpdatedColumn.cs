using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Devnet.Vault.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDetails_Email",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_PhoneNumber",
                table: "UserDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserDetails",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfileUrl",
                table: "UserDetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoleFeatures",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Features",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Countries",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_Email_IsDeleted",
                table: "UserDetails",
                columns: new[] { "Email", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_PhoneNumber_IsDeleted",
                table: "UserDetails",
                columns: new[] { "PhoneNumber", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId_IsDeleted",
                table: "UserDetails",
                columns: new[] { "UserId", "IsDeleted" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDetails_Email_IsDeleted",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_PhoneNumber_IsDeleted",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_UserId_IsDeleted",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "ProfileUrl",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoleFeatures");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Countries");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_Email",
                table: "UserDetails",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_PhoneNumber",
                table: "UserDetails",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
