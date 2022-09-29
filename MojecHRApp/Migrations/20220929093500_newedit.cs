using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojecHRApp.Migrations
{
    public partial class newedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsEdit",
                table: "LoginTbl",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdit",
                table: "LoginTbl");
        }
    }
}
