using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class afteraddingdriverpersonnelanddistributorpersonnelcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistributorPersonnelCode",
                table: "Shipments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverPersonnelCode",
                table: "Shipments",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistributorPersonnelCode",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DriverPersonnelCode",
                table: "Shipments");
        }
    }
}
