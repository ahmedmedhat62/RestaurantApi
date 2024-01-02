using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class final_a7a2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishBasketDTO_Baskets_BasketId",
                table: "DishBasketDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishBasketDTO",
                table: "DishBasketDTO");

            migrationBuilder.RenameTable(
                name: "DishBasketDTO",
                newName: "dishBasketDTOs");

            migrationBuilder.RenameIndex(
                name: "IX_DishBasketDTO_BasketId",
                table: "dishBasketDTOs",
                newName: "IX_dishBasketDTOs_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dishBasketDTOs",
                table: "dishBasketDTOs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dishBasketDTOs_Baskets_BasketId",
                table: "dishBasketDTOs",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dishBasketDTOs_Baskets_BasketId",
                table: "dishBasketDTOs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dishBasketDTOs",
                table: "dishBasketDTOs");

            migrationBuilder.RenameTable(
                name: "dishBasketDTOs",
                newName: "DishBasketDTO");

            migrationBuilder.RenameIndex(
                name: "IX_dishBasketDTOs_BasketId",
                table: "DishBasketDTO",
                newName: "IX_DishBasketDTO_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishBasketDTO",
                table: "DishBasketDTO",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishBasketDTO_Baskets_BasketId",
                table: "DishBasketDTO",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");
        }
    }
}
