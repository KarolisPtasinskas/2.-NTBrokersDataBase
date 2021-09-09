using Microsoft.EntityFrameworkCore.Migrations;

namespace _2._NTBrokersDataBase.Migrations
{
    public partial class RemoveCompanyBrokersIdAndOther : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyBrokers",
                table: "CompanyBrokers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CompanyBrokers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CompanyBrokers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyBrokers",
                table: "CompanyBrokers",
                column: "Id");
        }
    }
}
