using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping_System.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Order_Statuses_Order_StatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WeightSettings_WeightSetting_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Order_StatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order_StatusId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WeightSetting_Id",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Village_Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "VillageSetting_Id",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingSetting_Id",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SecoundPhoneNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Orders",
                type: "nvarchar(120)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status_Id",
                table: "Orders",
                column: "Status_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Order_Statuses_Status_Id",
                table: "Orders",
                column: "Status_Id",
                principalTable: "Order_Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders",
                column: "ShippingSetting_Id",
                principalTable: "ShippingSettings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeightSettings_WeightSetting_Id",
                table: "Orders",
                column: "WeightSetting_Id",
                principalTable: "WeightSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Order_Statuses_Status_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WeightSettings_WeightSetting_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Status_Id",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WeightSetting_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Village_Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VillageSetting_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShippingSetting_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecoundPhoneNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Orders",
                type: "nvarchar(120)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order_StatusId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Order_StatusId",
                table: "Orders",
                column: "Order_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Order_Statuses_Order_StatusId",
                table: "Orders",
                column: "Order_StatusId",
                principalTable: "Order_Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingSettings_ShippingSetting_Id",
                table: "Orders",
                column: "ShippingSetting_Id",
                principalTable: "ShippingSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WeightSettings_WeightSetting_Id",
                table: "Orders",
                column: "WeightSetting_Id",
                principalTable: "WeightSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
