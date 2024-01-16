using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class Order_Dto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderDTOId",
                table: "dishBasketDTOs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDTOs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    orderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDTOs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dishBasketDTOs_OrderDTOId",
                table: "dishBasketDTOs",
                column: "OrderDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_dishBasketDTOs_OrderDTOs_OrderDTOId",
                table: "dishBasketDTOs",
                column: "OrderDTOId",
                principalTable: "OrderDTOs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dishBasketDTOs_OrderDTOs_OrderDTOId",
                table: "dishBasketDTOs");

            migrationBuilder.DropTable(
                name: "OrderDTOs");

            migrationBuilder.DropIndex(
                name: "IX_dishBasketDTOs_OrderDTOId",
                table: "dishBasketDTOs");

            migrationBuilder.DropColumn(
                name: "OrderDTOId",
                table: "dishBasketDTOs");
        }
    }
}
