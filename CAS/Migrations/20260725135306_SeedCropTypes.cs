using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CAS.Migrations
{
    /// <inheritdoc />
    public partial class SeedCropTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("202d1e4d-4423-468f-9b78-84d2ee041b8b"),
                column: "UpdatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("57bfb05d-063b-4e84-86dd-76f90d83b6ac"),
                column: "UpdatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "SoilTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "SoilTypeStatus", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("297dac6b-ef82-429f-88bc-ef79f32c428d"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Contains a high amount of dead organic matter (humus) and is dark, spongy, and acidic. It acts like a sponge and holds a lot of water.", "Peaty Soil", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("32b43d41-7a17-4fea-86e1-e105e89db4be"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "The ideal agricultural soil, consisting of a balanced mixture of sand, silt, and clay. It is nutrient-rich, retains moisture effectively, and drains well.", "Loamy Soil", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("52f17eba-5f74-489f-89e0-e3c886146852"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Made of the smallest particles, making it sticky when wet and rock-hard when dry. It retains nutrients and moisture well but drains slowly.", "Clay Soil", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("82907dc8-96db-429e-9550-a7fcd4f4ce6a"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Composed of large particles, making it gritty to the touch. It drains very quickly and warms up fast in the spring, but holds few nutrients.", "Sandy Soil", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e8927783-5f40-443b-8db5-d12f42d9b399"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Made of medium-sized particles, feeling smooth like powder. It is highly fertile, retains moisture, and is often found near water bodies.", "Silty Soil", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fcc6c2a8-a714-4eee-800f-ede74c35f876"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Highly alkaline and contains visible stones or pieces of chalk. It is usually stony, free-draining, and requires organic matter to improve its fertility.", "Chalky Soil", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c9e1b2-5d4a-4e6b-8f1a-2c3d4e5f6a7b"),
                column: "UpdatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SoilTypes",
                keyColumn: "Id",
                keyValue: new Guid("297dac6b-ef82-429f-88bc-ef79f32c428d"));

            migrationBuilder.DeleteData(
                table: "SoilTypes",
                keyColumn: "Id",
                keyValue: new Guid("32b43d41-7a17-4fea-86e1-e105e89db4be"));

            migrationBuilder.DeleteData(
                table: "SoilTypes",
                keyColumn: "Id",
                keyValue: new Guid("52f17eba-5f74-489f-89e0-e3c886146852"));

            migrationBuilder.DeleteData(
                table: "SoilTypes",
                keyColumn: "Id",
                keyValue: new Guid("82907dc8-96db-429e-9550-a7fcd4f4ce6a"));

            migrationBuilder.DeleteData(
                table: "SoilTypes",
                keyColumn: "Id",
                keyValue: new Guid("e8927783-5f40-443b-8db5-d12f42d9b399"));

            migrationBuilder.DeleteData(
                table: "SoilTypes",
                keyColumn: "Id",
                keyValue: new Guid("fcc6c2a8-a714-4eee-800f-ede74c35f876"));

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
                column: "UpdatedAt",
                value: new DateTime(2026, 7, 14, 16, 29, 27, 120, DateTimeKind.Utc).AddTicks(4735));
        }
    }
}
