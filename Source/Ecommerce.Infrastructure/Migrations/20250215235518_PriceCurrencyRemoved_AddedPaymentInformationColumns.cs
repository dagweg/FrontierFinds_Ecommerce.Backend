using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PriceCurrencyRemoved_AddedPaymentInformationColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "PriceCurrency", table: "Products");

            migrationBuilder.DropColumn(name: "PriceCurrency", table: "OrderItems");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CardCVV", table: "Orders");

            migrationBuilder.DropColumn(name: "CardExpiryMonth", table: "Orders");

            migrationBuilder.DropColumn(name: "CardExpiryYear", table: "Orders");

            migrationBuilder.DropColumn(name: "CardHolderName", table: "Orders");

            migrationBuilder.DropColumn(name: "CardNumber", table: "Orders");

            migrationBuilder.AddColumn<string>(
              name: "PriceCurrency",
              table: "Products",
              type: "nvarchar(3)",
              maxLength: 3,
              nullable: false,
              defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
              name: "PriceCurrency",
              table: "OrderItems",
              type: "nvarchar(max)",
              nullable: false,
              defaultValue: ""
            );
        }
    }
}
