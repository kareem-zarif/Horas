using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horas.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProfiles_AspNetUsers_PersonId",
                table: "SellerProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProfiles_AspNetUsers_PersonId",
                table: "SellerProfiles",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProfiles_AspNetUsers_PersonId",
                table: "SellerProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProfiles_AspNetUsers_PersonId",
                table: "SellerProfiles",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
