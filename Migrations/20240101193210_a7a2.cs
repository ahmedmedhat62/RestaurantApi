using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class a7a2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "testStr",
                table: "Baskets");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dish_id",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_BasketId",
                table: "Dishes",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Baskets_BasketId",
                table: "Dishes",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Baskets_BasketId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_BasketId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "dish_id",
                table: "Baskets");

            migrationBuilder.AddColumn<string>(
                name: "testStr",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
