using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class removepersonnelandappuserconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Personnels_PersonnelId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PersonnelId",
                table: "AspNetUsers",
                newName: "PersonnelCode");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PersonnelId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PersonnelCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Personnels_PersonnelCode",
                table: "AspNetUsers",
                column: "PersonnelCode",
                principalTable: "Personnels",
                principalColumn: "PersonnelCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Personnels_PersonnelCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PersonnelCode",
                table: "AspNetUsers",
                newName: "PersonnelId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PersonnelCode",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Personnels_PersonnelId",
                table: "AspNetUsers",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "PersonnelCode");
        }
    }
}
