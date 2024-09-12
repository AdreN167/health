using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFoodPropertiesFromDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Carbohydrates",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Fats",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Proteins",
                table: "Dishes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Dishes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Carbohydrates",
                table: "Dishes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fats",
                table: "Dishes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Proteins",
                table: "Dishes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
