using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class PaymentAggregate : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "CardCVV", table: "Orders");

      migrationBuilder.DropColumn(name: "CardExpiryMonth", table: "Orders");

      migrationBuilder.DropColumn(name: "CardExpiryYear", table: "Orders");

      migrationBuilder.DropColumn(name: "CardHolderName", table: "Orders");

      migrationBuilder.DropColumn(name: "CardNumber", table: "Orders");

      migrationBuilder.CreateTable(
        name: "Payments",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
          PriceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
          PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
          OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          PayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
          FailureReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
          PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Payments", x => x.Id);
        }
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Payments");

      migrationBuilder.AddColumn<string>(
        name: "CardCVV",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<int>(
        name: "CardExpiryMonth",
        table: "Orders",
        type: "int",
        nullable: false,
        defaultValue: 0
      );

      migrationBuilder.AddColumn<int>(
        name: "CardExpiryYear",
        table: "Orders",
        type: "int",
        nullable: false,
        defaultValue: 0
      );

      migrationBuilder.AddColumn<string>(
        name: "CardHolderName",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "CardNumber",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: ""
      );
    }
  }
}
