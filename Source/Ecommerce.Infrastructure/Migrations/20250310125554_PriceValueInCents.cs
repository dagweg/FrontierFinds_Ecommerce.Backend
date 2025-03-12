using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class PriceValueInCents : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "PriceValue", table: "Products");

      migrationBuilder.DropColumn(name: "PriceValue", table: "Payments");

      migrationBuilder.AddColumn<long>(
        name: "Price_ValueInCents",
        table: "Products",
        type: "bigint",
        nullable: false,
        defaultValue: 0L
      );

      migrationBuilder.AddColumn<long>(
        name: "Price_ValueInCents",
        table: "Payments",
        type: "bigint",
        nullable: false,
        defaultValue: 0L
      );

      migrationBuilder.AlterColumn<long>(
        name: "PriceValue",
        table: "OrderItems",
        type: "bigint",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "decimal(18,2)"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "Price_ValueInCents", table: "Products");

      migrationBuilder.DropColumn(name: "Price_ValueInCents", table: "Payments");

      migrationBuilder.AddColumn<decimal>(
        name: "PriceValue",
        table: "Products",
        type: "decimal(18,2)",
        nullable: false,
        defaultValue: 0m
      );

      migrationBuilder.AddColumn<decimal>(
        name: "PriceValue",
        table: "Payments",
        type: "decimal(18,2)",
        nullable: false,
        defaultValue: 0m
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "PriceValue",
        table: "OrderItems",
        type: "decimal(18,2)",
        nullable: false,
        oldClrType: typeof(long),
        oldType: "bigint"
      );
    }
  }
}
