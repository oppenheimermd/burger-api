using Microsoft.EntityFrameworkCore.Migrations;

namespace BurgerApi.Migrations
{
    public partial class Tbl_Burger_correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BurgerId",
                table: "BurgerImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BurgerId",
                table: "BurgerImage",
                nullable: false,
                defaultValue: 0);
        }
    }
}
