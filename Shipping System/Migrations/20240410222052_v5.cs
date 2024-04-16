using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping_System.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Representitive_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingSetting_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trader_Id",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Trader_Id",
                table: "Orders",
                column: "Trader_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_Representitive_Id",
                table: "Orders",
                column: "Representitive_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_Trader_Id",
                table: "Orders",
                column: "Trader_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders",
                column: "ShippingSetting_Id",
                principalTable: "ShippingSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Representitive_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Trader_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Trader_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Trader_Id",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingSetting_Id",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_Representitive_Id",
                table: "Orders",
                column: "Representitive_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders",
                column: "ShippingSetting_Id",
                principalTable: "ShippingSettings",
                principalColumn: "Id");
        }
    }
}
