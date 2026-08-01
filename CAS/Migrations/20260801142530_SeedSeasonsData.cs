using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CAS.Migrations
{
    /// <inheritdoc />
    public partial class SeedSeasonsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "SeasonStatus", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("36bf3939-a5d0-4e37-8347-dabb8c33404c"), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Dry Season", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("813b1b0b-045e-4f12-89fb-e602dfa4e84d"), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Rainy Season", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: new Guid("36bf3939-a5d0-4e37-8347-dabb8c33404c"));

            migrationBuilder.DeleteData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: new Guid("813b1b0b-045e-4f12-89fb-e602dfa4e84d"));
        }
    }
}
