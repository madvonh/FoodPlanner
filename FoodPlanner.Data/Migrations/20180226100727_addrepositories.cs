using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodPlanner.Data.Migrations
{
    public partial class addrepositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryGrocery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(nullable: true),
                    SystemName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGrocery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LongDescription = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groceries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryGroceryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RecipeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groceries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groceries_CategoryGrocery_CategoryGroceryId",
                        column: x => x.CategoryGroceryId,
                        principalTable: "CategoryGrocery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groceries_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PieceOfJob",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceOfJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PieceOfJob_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeInfoRecipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false),
                    RecipeInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInfoRecipe", x => new { x.RecipeId, x.RecipeInfoId });
                    table.ForeignKey(
                        name: "FK_RecipeInfoRecipe_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeInfoRecipe_RecipeInfos_RecipeInfoId",
                        column: x => x.RecipeInfoId,
                        principalTable: "RecipeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groceries_CategoryGroceryId",
                table: "Groceries",
                column: "CategoryGroceryId");

            migrationBuilder.CreateIndex(
                name: "IX_Groceries_RecipeId",
                table: "Groceries",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_PieceOfJob_RecipeId",
                table: "PieceOfJob",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInfoRecipe_RecipeInfoId",
                table: "RecipeInfoRecipe",
                column: "RecipeInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groceries");

            migrationBuilder.DropTable(
                name: "PieceOfJob");

            migrationBuilder.DropTable(
                name: "RecipeInfoRecipe");

            migrationBuilder.DropTable(
                name: "CategoryGrocery");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
