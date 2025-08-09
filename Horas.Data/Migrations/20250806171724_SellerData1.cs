using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horas.Data.Migrations
{
    /// <inheritdoc />
    public partial class SellerData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProfile_AspNetUsers_PersonId",
                table: "SellerProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerProfile",
                table: "SellerProfile");

            migrationBuilder.RenameTable(
                name: "SellerProfile",
                newName: "SellerProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_SellerProfile_PersonId",
                table: "SellerProfiles",
                newName: "IX_SellerProfiles_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerProfiles",
                table: "SellerProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProfiles_AspNetUsers_PersonId",
                table: "SellerProfiles",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProfiles_AspNetUsers_PersonId",
                table: "SellerProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerProfiles",
                table: "SellerProfiles");

            migrationBuilder.RenameTable(
                name: "SellerProfiles",
                newName: "SellerProfile");

            migrationBuilder.RenameIndex(
                name: "IX_SellerProfiles_PersonId",
                table: "SellerProfile",
                newName: "IX_SellerProfile_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerProfile",
                table: "SellerProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProfile_AspNetUsers_PersonId",
                table: "SellerProfile",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
