using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class slug : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
        name: "Slug",
        table: "Products",
        type: "nvarchar(450)",
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.CreateIndex(
        name: "IX_Products_Slug",
        table: "Products",
        column: "Slug",
        unique: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(name: "IX_Products_Slug", table: "Products");

      migrationBuilder.DropColumn(name: "Slug", table: "Products");
    }
  }
}
