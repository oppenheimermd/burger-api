using Microsoft.EntityFrameworkCore.Migrations;

namespace BurgerApi.Migrations
{
    public partial class Tbl_corrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSourceUrl",
                table: "BurgerImage");

            migrationBuilder.AddColumn<int>(
                name: "BurgerId",
                table: "BurgerImage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BurgerId",
                table: "BurgerImage");

            migrationBuilder.AddColumn<string>(
                name: "ImageSourceUrl",
                table: "BurgerImage",
                nullable: false,
                defaultValue: "");
        }
    }
}
