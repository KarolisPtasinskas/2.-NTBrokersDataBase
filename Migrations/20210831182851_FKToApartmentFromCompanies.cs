using Microsoft.EntityFrameworkCore.Migrations;

namespace _2._NTBrokersDataBase.Migrations
{
    public partial class FKToApartmentFromCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Apartments_CompanyId",
                table: "Apartments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Companies_CompanyId",
                table: "Apartments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Companies_CompanyId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_CompanyId",
                table: "Apartments");
        }
    }
}
