using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibBooking.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a82d7d58-2b50-4159-aaa8-7b065c911c2e", "a7c7eeb3-53d9-421a-91a8-f0c3fd3c7f57" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a82d7d58-2b50-4159-aaa8-7b065c911c2e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7c7eeb3-53d9-421a-91a8-f0c3fd3c7f57");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d0a9ba4-86b4-476b-9669-62cae1c9f75f", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "af4bc152-8e08-4e64-bc72-9e46aca1caa5", 0, "ab2fa1bd-665c-4932-adf8-0a6dd0b4417d", "admin@library.com", true, false, null, "ADMIN@LIBRARY.COM", "ADMIN@LIBRARY.COM", "AQAAAAIAAYagAAAAECDz04bWIhk0nNGxoR+wTyDQ3fB1OzglIvxCOSCPX8qkyPsQ3AtIwUzuj2Zv2o79gQ==", null, false, "3671d804-64de-4a1f-8589-6614caa8058d", false, "admin@library.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0d0a9ba4-86b4-476b-9669-62cae1c9f75f", "af4bc152-8e08-4e64-bc72-9e46aca1caa5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0d0a9ba4-86b4-476b-9669-62cae1c9f75f", "af4bc152-8e08-4e64-bc72-9e46aca1caa5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d0a9ba4-86b4-476b-9669-62cae1c9f75f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af4bc152-8e08-4e64-bc72-9e46aca1caa5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a82d7d58-2b50-4159-aaa8-7b065c911c2e", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a7c7eeb3-53d9-421a-91a8-f0c3fd3c7f57", 0, "0fced0d7-adff-44ff-9196-3962bc42019e", "admin@library.com", true, false, null, "ADMIN@LIBRARY.COM", "ADMIN@LIBRARY.COM", "AQAAAAIAAYagAAAAEAsmJAQjwv9etsAxaP+Oa9ggtOxl//yd1eYzmv6afjP7hN9mBjhQjXKRjgttd5K+OQ==", null, false, "958cb070-fa8b-48e2-be4b-46ace7758671", false, "admin@library.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a82d7d58-2b50-4159-aaa8-7b065c911c2e", "a7c7eeb3-53d9-421a-91a8-f0c3fd3c7f57" });
        }
    }
}
