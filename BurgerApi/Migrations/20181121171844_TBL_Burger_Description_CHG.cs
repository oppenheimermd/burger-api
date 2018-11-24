using Microsoft.EntityFrameworkCore.Migrations;

namespace BurgerApi.Migrations
{
    public partial class TBL_Burger_Description_CHG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Burger",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 140,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Burger",
                maxLength: 140,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
