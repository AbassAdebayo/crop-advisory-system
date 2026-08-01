using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAS.Migrations
{
    /// <inheritdoc />
    public partial class AdvisoryFieldsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantingTips",
                table: "Advisories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlantingTips",
                table: "Advisories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
