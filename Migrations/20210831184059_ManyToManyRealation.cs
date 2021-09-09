using Microsoft.EntityFrameworkCore.Migrations;

namespace _2._NTBrokersDataBase.Migrations
{
    public partial class ManyToManyRealation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyBrokers_CompanyId",
                table: "CompanyBrokers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyBrokers",
                table: "CompanyBrokers",
                columns: new[] { "CompanyId", "BrokerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyBrokers",
                table: "CompanyBrokers");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBrokers_CompanyId",
                table: "CompanyBrokers",
                column: "CompanyId");
        }
    }
}
