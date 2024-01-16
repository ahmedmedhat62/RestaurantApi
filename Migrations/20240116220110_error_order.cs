using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class error_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "useremail",
                table: "dishBasketDTOs");

            migrationBuilder.AddColumn<string>(
                name: "useremail",
                table: "OrderDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "useremail",
                table: "OrderDTOs");

            migrationBuilder.AddColumn<string>(
                name: "useremail",
                table: "dishBasketDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
