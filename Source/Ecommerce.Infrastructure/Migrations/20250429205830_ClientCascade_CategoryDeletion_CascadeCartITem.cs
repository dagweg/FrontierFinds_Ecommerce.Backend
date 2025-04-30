using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientCascade_CategoryDeletion_CascadeCartITem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCartItems_Products_ProductId",
                table: "UserCartItems");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCartItems_Products_ProductId",
                table: "UserCartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCartItems_Products_ProductId",
                table: "UserCartItems");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCartItems_Products_ProductId",
                table: "UserCartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
