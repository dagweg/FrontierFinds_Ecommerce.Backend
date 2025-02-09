using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class FixedUserCart : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(name: "IX_UserCarts_UserId", table: "UserCarts");

      migrationBuilder.CreateIndex(
        name: "IX_UserCarts_UserId",
        table: "UserCarts",
        column: "UserId",
        unique: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(name: "IX_UserCarts_UserId", table: "UserCarts");

      migrationBuilder.CreateIndex(
        name: "IX_UserCarts_UserId",
        table: "UserCarts",
        column: "UserId"
      );
    }
  }
}
