using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping_System.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Shipping_Total_Cost",
                table: "Orders",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Order_Total_Cost",
                table: "Orders",
                type: "money",
                nullable: false,
                computedColumnSql: "[Shipping_Total_Cost] + [Products_Total_Cost]",
                oldClrType: typeof(decimal),
                oldType: "money");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shipping_Total_Cost",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "Order_Total_Cost",
                table: "Orders",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldComputedColumnSql: "[Shipping_Total_Cost] + [Products_Total_Cost]");
        }
    }
}
