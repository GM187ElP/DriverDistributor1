using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class addingusernametoshipmentfrompersonnel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_UserName",
                table: "Shipments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Personnels_Name",
                table: "Personnels",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ApplicationUserId",
                table: "Shipments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_Name",
                table: "Personnels",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_AspNetUsers_ApplicationUserId",
                table: "Shipments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Personnels_UserName",
                table: "Shipments",
                column: "UserName",
                principalTable: "Personnels",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_ApplicationUserId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Personnels_UserName",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ApplicationUserId",
                table: "Shipments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Personnels_Name",
                table: "Personnels");

            migrationBuilder.DropIndex(
                name: "IX_Personnels_Name",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_AspNetUsers_UserName",
                table: "Shipments",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
