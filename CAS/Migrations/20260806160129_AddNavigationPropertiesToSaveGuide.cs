using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAS.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationPropertiesToSaveGuide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SaveGuides",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: new Guid("36bf3939-a5d0-4e37-8347-dabb8c33404c"),
                column: "Name",
                value: "Dry");

            migrationBuilder.UpdateData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: new Guid("813b1b0b-045e-4f12-89fb-e602dfa4e84d"),
                column: "Name",
                value: "Rainy");

            migrationBuilder.CreateIndex(
                name: "IX_SaveGuides_AdvisoryId",
                table: "SaveGuides",
                column: "AdvisoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveGuides_UserId",
                table: "SaveGuides",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaveGuides_Advisories_AdvisoryId",
                table: "SaveGuides",
                column: "AdvisoryId",
                principalTable: "Advisories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaveGuides_Users_UserId",
                table: "SaveGuides",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaveGuides_Advisories_AdvisoryId",
                table: "SaveGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_SaveGuides_Users_UserId",
                table: "SaveGuides");

            migrationBuilder.DropIndex(
                name: "IX_SaveGuides_AdvisoryId",
                table: "SaveGuides");

            migrationBuilder.DropIndex(
                name: "IX_SaveGuides_UserId",
                table: "SaveGuides");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SaveGuides");

            migrationBuilder.UpdateData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: new Guid("36bf3939-a5d0-4e37-8347-dabb8c33404c"),
                column: "Name",
                value: "Dry Season");

            migrationBuilder.UpdateData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: new Guid("813b1b0b-045e-4f12-89fb-e602dfa4e84d"),
                column: "Name",
                value: "Rainy Season");
        }
    }
}
