using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BurgerApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BurgerBase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseName = table.Column<string>(nullable: false),
                    BaseNameCode = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BurgerImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileNameLarge = table.Column<string>(nullable: false),
                    FileNameMedium = table.Column<string>(nullable: false),
                    FileNameSmall = table.Column<string>(nullable: false),
                    ImageSourceUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuisine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CuisineCode = table.Column<string>(maxLength: 4, nullable: false),
                    CuisineTitle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Burger",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 140, nullable: true),
                    InstagramUserId = table.Column<string>(maxLength: 70, nullable: false),
                    InstagramSourceUrl = table.Column<string>(nullable: false),
                    Verified = table.Column<bool>(nullable: false),
                    CuisineId = table.Column<int>(nullable: false),
                    BurgerBaseId = table.Column<int>(nullable: false),
                    BurgerImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Burger_BurgerBase_BurgerBaseId",
                        column: x => x.BurgerBaseId,
                        principalTable: "BurgerBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Burger_BurgerImage_BurgerImageId",
                        column: x => x.BurgerImageId,
                        principalTable: "BurgerImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Burger_Cuisine_CuisineId",
                        column: x => x.CuisineId,
                        principalTable: "Cuisine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Burger_BurgerBaseId",
                table: "Burger",
                column: "BurgerBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Burger_BurgerImageId",
                table: "Burger",
                column: "BurgerImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Burger_CuisineId",
                table: "Burger",
                column: "CuisineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Burger");

            migrationBuilder.DropTable(
                name: "BurgerBase");

            migrationBuilder.DropTable(
                name: "BurgerImage");

            migrationBuilder.DropTable(
                name: "Cuisine");
        }
    }
}
