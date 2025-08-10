using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class adddutytoshipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistributorDuty",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverDuty",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonthName",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistributorDuty",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DriverDuty",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "MonthName",
                table: "Shipments");
        }
    }
}
