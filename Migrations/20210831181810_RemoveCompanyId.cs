using Microsoft.EntityFrameworkCore.Migrations;

namespace _2._NTBrokersDataBase.Migrations
{
    public partial class RemoveCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brokers_Companies_CompanyId",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Brokers_CompanyId",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Brokers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Brokers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_CompanyId",
                table: "Brokers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brokers_Companies_CompanyId",
                table: "Brokers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
