using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations.MealPlannerDbContextSqLiteMigrations
{
    public partial class AddBookNameTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageNumber",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PageNumber",
                table: "Recipes");
        }
    }
}
