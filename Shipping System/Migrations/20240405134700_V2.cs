using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping_System.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order_Status",
                table: "Orders",
                newName: "Status_Id");

            migrationBuilder.AddColumn<int>(
                name: "Order_StatusId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order_Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Statuses", x => x.Id);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Order_Statuses_Order_StatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Order_Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Order_StatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order_StatusId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Status_Id",
                table: "Orders",
                newName: "Order_Status");
        }
    }
}
