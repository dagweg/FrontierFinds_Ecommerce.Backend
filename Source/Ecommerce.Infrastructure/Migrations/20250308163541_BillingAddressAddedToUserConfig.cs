using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class BillingAddressAddedToUserConfig : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
        name: "BillingAddress_City",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true
      );

      migrationBuilder.AddColumn<string>(
        name: "BillingAddress_Country",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true
      );

      migrationBuilder.AddColumn<string>(
        name: "BillingAddress_State",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true
      );

      migrationBuilder.AddColumn<string>(
        name: "BillingAddress_Street",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true
      );

      migrationBuilder.AddColumn<string>(
        name: "BillingAddress_ZipCode",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "BillingAddress_City", table: "Users");

      migrationBuilder.DropColumn(name: "BillingAddress_Country", table: "Users");

      migrationBuilder.DropColumn(name: "BillingAddress_State", table: "Users");

      migrationBuilder.DropColumn(name: "BillingAddress_Street", table: "Users");

      migrationBuilder.DropColumn(name: "BillingAddress_ZipCode", table: "Users");
    }
  }
}
