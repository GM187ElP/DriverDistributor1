using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class changepropstonull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Distributors_DistributorName",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Drivers_DriverName",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "DriverName",
                table: "Shipments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DistributorName",
                table: "Shipments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Distributors_DistributorName",
                table: "Shipments",
                column: "DistributorName",
                principalTable: "Distributors",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Drivers_DriverName",
                table: "Shipments",
                column: "DriverName",
                principalTable: "Drivers",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Distributors_DistributorName",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Drivers_DriverName",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "DriverName",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistributorName",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Distributors_DistributorName",
                table: "Shipments",
                column: "DistributorName",
                principalTable: "Distributors",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Drivers_DriverName",
                table: "Shipments",
                column: "DriverName",
                principalTable: "Drivers",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
