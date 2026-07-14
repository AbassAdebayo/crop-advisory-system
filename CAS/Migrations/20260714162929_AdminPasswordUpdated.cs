using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAS.Migrations
{
    /// <inheritdoc />
    public partial class AdminPasswordUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("202d1e4d-4423-468f-9b78-84d2ee041b8b"),
                column: "UpdatedAt",
                value: new DateTime(2026, 7, 14, 16, 29, 27, 119, DateTimeKind.Utc).AddTicks(9641));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("57bfb05d-063b-4e84-86dd-76f90d83b6ac"),
                column: "UpdatedAt",
                value: new DateTime(2026, 7, 14, 16, 29, 27, 125, DateTimeKind.Utc).AddTicks(6119));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c9e1b2-5d4a-4e6b-8f1a-2c3d4e5f6a7b"),
                columns: new[] { "PasswordHash", "UpdatedAt" },
                values: new object[] { "AQAAAAIAAYagAAAAEH57jLQ7uc7oKhUYtas/A3EDzs8yY13z1jMAlgZiR+WJAsOxqgsbo0y+3ztTRUCmjA==", new DateTime(2026, 7, 14, 16, 29, 27, 120, DateTimeKind.Utc).AddTicks(4735) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("202d1e4d-4423-468f-9b78-84d2ee041b8b"),
                column: "UpdatedAt",
                value: new DateTime(2026, 6, 24, 16, 17, 17, 215, DateTimeKind.Utc).AddTicks(2312));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("57bfb05d-063b-4e84-86dd-76f90d83b6ac"),
                column: "UpdatedAt",
                value: new DateTime(2026, 6, 24, 16, 17, 17, 222, DateTimeKind.Utc).AddTicks(3226));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c9e1b2-5d4a-4e6b-8f1a-2c3d4e5f6a7b"),
                columns: new[] { "PasswordHash", "UpdatedAt" },
                values: new object[] { "AQAAAAEAACcQAAAAEP55WXadi3LFl/WUHm61QFIdM7BF33w0jUBWix6x/RFfzvK8F0VN4/KNkLFlDuMdEg==", new DateTime(2026, 6, 24, 16, 17, 17, 215, DateTimeKind.Utc).AddTicks(8058) });
        }
    }
}
