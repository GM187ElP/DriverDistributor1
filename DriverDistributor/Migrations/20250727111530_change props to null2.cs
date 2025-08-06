using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class changepropstonull2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Warehouses_WarehouseName",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "Shipments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Warehouses_WarehouseName",
                table: "Shipments",
                column: "WarehouseName",
                principalTable: "Warehouses",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Warehouses_WarehouseName",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Warehouses_WarehouseName",
                table: "Shipments",
                column: "WarehouseName",
                principalTable: "Warehouses",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
