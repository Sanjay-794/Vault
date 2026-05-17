using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Devnet.Vault.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryCallingCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FeatureName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubfeatureId = table.Column<int>(type: "int", nullable: true),
                    FeatureGroup = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                    table.ForeignKey(
                        name: "FK_Features_Features_SubfeatureId",
                        column: x => x.SubfeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupDetails",
                columns: table => new
                {
                    GroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentGroupId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    IsFavourite = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    MetadataJson = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDetails", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_GroupDetails_GroupDetails_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "GroupDetails",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleFeatures",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleFeatures", x => new { x.RoleId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_RoleFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleFeatures_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    ProfileUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoginDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeactivated = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeactivatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserDetails_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDetails_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserLoginId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RefreshTokenHash = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    IpAddress = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserLoginId);
                    table.ForeignKey(
                        name: "FK_UserLogins_UserDetails_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDetails",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VaultEntries",
                columns: table => new
                {
                    VaultEntryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    EncryptedData = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    IsFavourite = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    GroupDetailsGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultEntries", x => x.VaultEntryId);
                    table.ForeignKey(
                        name: "FK_VaultEntries_GroupDetails_GroupDetailsGroupId",
                        column: x => x.GroupDetailsGroupId,
                        principalTable: "GroupDetails",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_VaultEntries_GroupDetails_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupDetails",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VaultEntries_UserDetails_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserDetails",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VaultFiles",
                columns: table => new
                {
                    VaultFileId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileKey = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Extension = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    IsFavourite = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    MetadataJson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupDetailsGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultFiles", x => x.VaultFileId);
                    table.ForeignKey(
                        name: "FK_VaultFiles_GroupDetails_GroupDetailsGroupId",
                        column: x => x.GroupDetailsGroupId,
                        principalTable: "GroupDetails",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_VaultFiles_GroupDetails_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupDetails",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VaultFiles_UserDetails_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserDetails",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryCallingCode",
                table: "Countries",
                column: "CountryCallingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryCode",
                table: "Countries",
                column: "CountryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_SubfeatureId",
                table: "Features",
                column: "SubfeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_OwnerId",
                table: "GroupDetails",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_OwnerId_ParentGroupId_Name",
                table: "GroupDetails",
                columns: new[] { "OwnerId", "ParentGroupId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupDetails_ParentGroupId",
                table: "GroupDetails",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFeatures_FeatureId",
                table: "RoleFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_CountryId",
                table: "UserDetails",
                column: "CountryId");

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
                name: "IX_UserDetails_RoleId",
                table: "UserDetails",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_UserId_IsDeleted",
                table: "UserDetails",
                columns: new[] { "UserId", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_RefreshTokenHash",
                table: "UserLogins",
                column: "RefreshTokenHash");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultEntries_EntryType",
                table: "VaultEntries",
                column: "EntryType");

            migrationBuilder.CreateIndex(
                name: "IX_VaultEntries_GroupDetailsGroupId",
                table: "VaultEntries",
                column: "GroupDetailsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultEntries_GroupId",
                table: "VaultEntries",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultEntries_OwnerId",
                table: "VaultEntries",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultEntries_OwnerId_GroupId_Title",
                table: "VaultEntries",
                columns: new[] { "OwnerId", "GroupId", "Title" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaultFiles_FileKey",
                table: "VaultFiles",
                column: "FileKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaultFiles_GroupDetailsGroupId",
                table: "VaultFiles",
                column: "GroupDetailsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultFiles_GroupId",
                table: "VaultFiles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultFiles_OwnerId",
                table: "VaultFiles",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleFeatures");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "VaultEntries");

            migrationBuilder.DropTable(
                name: "VaultFiles");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "GroupDetails");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
