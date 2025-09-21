using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightToDietsFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietDish_Diets_DietsId",
                table: "DietDish");

            migrationBuilder.DropForeignKey(
                name: "FK_DietDish_Dishes_DishesId",
                table: "DietDish");

            migrationBuilder.DropForeignKey(
                name: "FK_DietProduct_Diets_DietsId",
                table: "DietProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DietProduct_Products_ProductsId",
                table: "DietProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "DietProduct",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "DietsId",
                table: "DietProduct",
                newName: "DietId");

            migrationBuilder.RenameIndex(
                name: "IX_DietProduct_ProductsId",
                table: "DietProduct",
                newName: "IX_DietProduct_ProductId");

            migrationBuilder.RenameColumn(
                name: "DishesId",
                table: "DietDish",
                newName: "DishId");

            migrationBuilder.RenameColumn(
                name: "DietsId",
                table: "DietDish",
                newName: "DietId");

            migrationBuilder.RenameIndex(
                name: "IX_DietDish_DishesId",
                table: "DietDish",
                newName: "IX_DietDish_DishId");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "DietProduct",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "DietDish",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_DietDish_Diets_DietId",
                table: "DietDish",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietDish_Dishes_DishId",
                table: "DietDish",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietProduct_Diets_DietId",
                table: "DietProduct",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietProduct_Products_ProductId",
                table: "DietProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietDish_Diets_DietId",
                table: "DietDish");

            migrationBuilder.DropForeignKey(
                name: "FK_DietDish_Dishes_DishId",
                table: "DietDish");

            migrationBuilder.DropForeignKey(
                name: "FK_DietProduct_Diets_DietId",
                table: "DietProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DietProduct_Products_ProductId",
                table: "DietProduct");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DietProduct");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DietDish");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "DietProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "DietId",
                table: "DietProduct",
                newName: "DietsId");

            migrationBuilder.RenameIndex(
                name: "IX_DietProduct_ProductId",
                table: "DietProduct",
                newName: "IX_DietProduct_ProductsId");

            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "DietDish",
                newName: "DishesId");

            migrationBuilder.RenameColumn(
                name: "DietId",
                table: "DietDish",
                newName: "DietsId");

            migrationBuilder.RenameIndex(
                name: "IX_DietDish_DishId",
                table: "DietDish",
                newName: "IX_DietDish_DishesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietDish_Diets_DietsId",
                table: "DietDish",
                column: "DietsId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietDish_Dishes_DishesId",
                table: "DietDish",
                column: "DishesId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietProduct_Diets_DietsId",
                table: "DietProduct",
                column: "DietsId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietProduct_Products_ProductsId",
                table: "DietProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
