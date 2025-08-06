using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class afteraddingshipmentnumbertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentNumbers",
                table: "Shipments");

            migrationBuilder.CreateTable(
                name: "ShipmentNumbers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    ShipmentId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentNumbers_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentNumbers_ShipmentId",
                table: "ShipmentNumbers",
                column: "ShipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentNumbers");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentNumbers",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
