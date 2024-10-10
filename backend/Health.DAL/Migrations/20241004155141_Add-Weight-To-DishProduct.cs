using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightToDishProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishProduct_Dishes_DishesId",
                table: "DishProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DishProduct_Products_ProductsId",
                table: "DishProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "DishProduct",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "DishesId",
                table: "DishProduct",
                newName: "DishId");

            migrationBuilder.RenameIndex(
                name: "IX_DishProduct_ProductsId",
                table: "DishProduct",
                newName: "IX_DishProduct_ProductId");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "DishProduct",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_DishProduct_Dishes_DishId",
                table: "DishProduct",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishProduct_Products_ProductId",
                table: "DishProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishProduct_Dishes_DishId",
                table: "DishProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_DishProduct_Products_ProductId",
                table: "DishProduct");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DishProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "DishProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "DishProduct",
                newName: "DishesId");

            migrationBuilder.RenameIndex(
                name: "IX_DishProduct_ProductId",
                table: "DishProduct",
                newName: "IX_DishProduct_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishProduct_Dishes_DishesId",
                table: "DishProduct",
                column: "DishesId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishProduct_Products_ProductsId",
                table: "DishProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
