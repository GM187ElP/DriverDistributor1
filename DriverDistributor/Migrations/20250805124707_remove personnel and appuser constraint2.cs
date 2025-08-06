using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class removepersonnelandappuserconstraint2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Personnels_PersonnelCode",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonnelCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonnelCode",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonnelCode",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonnelCode",
                table: "AspNetUsers",
                column: "PersonnelCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Personnels_PersonnelCode",
                table: "AspNetUsers",
                column: "PersonnelCode",
                principalTable: "Personnels",
                principalColumn: "PersonnelCode");
        }
    }
}
