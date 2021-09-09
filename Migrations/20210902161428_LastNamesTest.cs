using Microsoft.EntityFrameworkCore.Migrations;

namespace _2._NTBrokersDataBase.Migrations
{
    public partial class LastNamesTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Brokers",
                newName: "LastNames");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastNames",
                table: "Brokers",
                newName: "LastName");
        }
    }
}
