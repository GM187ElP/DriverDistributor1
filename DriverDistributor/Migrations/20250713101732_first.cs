using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsExt = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShipmentDateGregorian = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ShipmentDatePersian = table.Column<string>(type: "TEXT", nullable: false),
                    Weekday = table.Column<string>(type: "TEXT", nullable: false),
                    DriverName = table.Column<string>(type: "TEXT", nullable: false),
                    DistributorName = table.Column<string>(type: "TEXT", nullable: false),
                    RouteName = table.Column<string>(type: "TEXT", nullable: false),
                    WarehouseName = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceCount = table.Column<int>(type: "INTEGER", nullable: true),
                    InvoiceAmount = table.Column<long>(type: "INTEGER", nullable: true),
                    ShipmentNumbers = table.Column<string>(type: "TEXT", nullable: false),
                    ReturnInvoiceCount = table.Column<int>(type: "INTEGER", nullable: true),
                    ReturnInvoiceAmount = table.Column<long>(type: "INTEGER", nullable: true),
                    SecondServiceInvoiceCount = table.Column<int>(type: "INTEGER", nullable: true),
                    ThirdServiceInvoiceCount = table.Column<int>(type: "INTEGER", nullable: true),
                    SecondServiceInvoiceAmount = table.Column<long>(type: "INTEGER", nullable: true),
                    ThirdServiceInvoiceAmount = table.Column<long>(type: "INTEGER", nullable: true),
                    HasVip = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsException = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Distributors_DistributorName",
                        column: x => x.DistributorName,
                        principalTable: "Distributors",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Drivers_DriverName",
                        column: x => x.DriverName,
                        principalTable: "Drivers",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Routes_RouteName",
                        column: x => x.RouteName,
                        principalTable: "Routes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Warehouses_WarehouseName",
                        column: x => x.WarehouseName,
                        principalTable: "Warehouses",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_DistributorName",
                table: "Shipments",
                column: "DistributorName");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_DriverName",
                table: "Shipments",
                column: "DriverName");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_RouteName",
                table: "Shipments",
                column: "RouteName");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_WarehouseName",
                table: "Shipments",
                column: "WarehouseName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
