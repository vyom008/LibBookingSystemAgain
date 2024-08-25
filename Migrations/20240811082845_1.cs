using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibBooking.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "32435448-9afa-4149-8676-b45eb846c713", "b021a8e6-153e-4a09-af9f-c472be9ca543" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32435448-9afa-4149-8676-b45eb846c713");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b021a8e6-153e-4a09-af9f-c472be9ca543");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "32435448-9afa-4149-8676-b45eb846c713", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b021a8e6-153e-4a09-af9f-c472be9ca543", 0, "03c57dc2-f3f5-4730-b0d9-1cba8e29ef4f", "admin@library.com", true, false, null, "ADMIN@LIBRARY.COM", "ADMIN@LIBRARY.COM", "AQAAAAIAAYagAAAAEFE1d9/7PJOlW+nGklVvguxU+pCNZBNW0MNIgHCe9c+fQOxVDbPit6nonxDqWzTPkQ==", null, false, "4f7dc6c4-5473-4e76-b67e-ba74b4c3846c", false, "admin@library.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "32435448-9afa-4149-8676-b45eb846c713", "b021a8e6-153e-4a09-af9f-c472be9ca543" });
        }
    }
}
