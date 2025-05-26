using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changing_DeliveyMethods_table_name_to_DeliveryMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveyMethods_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveyMethods",
                table: "DeliveyMethods");

            migrationBuilder.RenameTable(
                name: "DeliveyMethods",
                newName: "DeliveryMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMethods",
                table: "DeliveryMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMethods",
                table: "DeliveryMethods");

            migrationBuilder.RenameTable(
                name: "DeliveryMethods",
                newName: "DeliveyMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveyMethods",
                table: "DeliveyMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveyMethods_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId",
                principalTable: "DeliveyMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
