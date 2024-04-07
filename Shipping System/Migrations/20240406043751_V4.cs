using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping_System.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_VillageSettings_VillageShippingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VillageShippingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VillageShippingId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Representitive_Id",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Representitive_Id",
                table: "Orders",
                column: "Representitive_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VillageSetting_Id",
                table: "Orders",
                column: "VillageSetting_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_Representitive_Id",
                table: "Orders",
                column: "Representitive_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_VillageSettings_VillageSetting_Id",
                table: "Orders",
                column: "VillageSetting_Id",
                principalTable: "VillageSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Representitive_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_VillageSettings_VillageSetting_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Representitive_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VillageSetting_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Representitive_Id",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "VillageShippingId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VillageShippingId",
                table: "Orders",
                column: "VillageShippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_VillageSettings_VillageShippingId",
                table: "Orders",
                column: "VillageShippingId",
                principalTable: "VillageSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
