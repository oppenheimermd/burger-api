using Microsoft.EntityFrameworkCore.Migrations;

namespace BurgerApi.Migrations
{
    public partial class Burger_InstagramUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Burger");

            migrationBuilder.AddColumn<string>(
                name: "InstagramSourceUrl",
                table: "Burger",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstagramUserId",
                table: "Burger",
                maxLength: 70,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstagramSourceUrl",
                table: "Burger");

            migrationBuilder.DropColumn(
                name: "InstagramUserId",
                table: "Burger");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Burger",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
