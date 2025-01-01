using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class Test_Migration_2 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
        name: "Address_Country",
        table: "Users",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "City",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "CountryCode",
        table: "Users",
        type: "nvarchar(3)",
        maxLength: 3,
        nullable: false,
        defaultValue: "251"
      );

      migrationBuilder.AddColumn<string>(
        name: "State",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "Street",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "ZipCode",
        table: "Users",
        type: "nvarchar(10)",
        maxLength: 10,
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.CreateTable(
        name: "Notifications",
        columns: table => new
        {
          NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
          Description = table.Column<string>(
            type: "nvarchar(1000)",
            maxLength: 1000,
            nullable: false
          ),
          NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
          NotificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
          ReadAt = table.Column<DateTime>(type: "datetime2", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Notifications", x => new { x.NotificationId, x.UserId });
          table.ForeignKey(
            name: "FK_Notifications_Users_UserId",
            column: x => x.UserId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "Orders",
        columns: table => new
        {
          OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
          UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
          Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
          ShippingStreet = table.Column<string>(
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false
          ),
          ShippingCity = table.Column<string>(
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false
          ),
          ShippingState = table.Column<string>(
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false
          ),
          ShippingCountry = table.Column<string>(
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false
          ),
          ShippingZipCode = table.Column<string>(
            type: "nvarchar(10)",
            maxLength: 10,
            nullable: false
          ),
          BillingStreet = table.Column<string>(
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false
          ),
          BillingCity = table.Column<string>(
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false
          ),
          BillingState = table.Column<string>(
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false
          ),
          BillingCountry = table.Column<string>(
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false
          ),
          BillingZipCode = table.Column<string>(
            type: "nvarchar(10)",
            maxLength: 10,
            nullable: false
          ),
          Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Orders", x => x.OrderId);
          table.ForeignKey(
            name: "FK_Orders_Users_UserId",
            column: x => x.UserId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "UserCarts",
        columns: table => new
        {
          CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_UserCarts", x => x.CartId);
          table.ForeignKey(
            name: "FK_UserCarts_Users_UserId",
            column: x => x.UserId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "Wishlists",
        columns: table => new
        {
          WishlistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Wishlists", x => x.WishlistId);
          table.ForeignKey(
            name: "FK_Wishlists_Users_UserId",
            column: x => x.UserId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "OrderItems",
        columns: table => new
        {
          OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Quantity = table.Column<int>(type: "int", nullable: false),
          PriceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
          PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_OrderItems", x => new { x.OrderItemId, x.OrderId });
          table.ForeignKey(
            name: "FK_OrderItems_Orders_OrderId",
            column: x => x.OrderId,
            principalTable: "Orders",
            principalColumn: "OrderId",
            onDelete: ReferentialAction.Cascade
          );
          table.ForeignKey(
            name: "FK_OrderItems_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "UserCartItems",
        columns: table => new
        {
          CartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Quantity = table.Column<int>(type: "int", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_UserCartItems", x => new { x.CartItemId, x.CartId });
          table.ForeignKey(
            name: "FK_UserCartItems_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
          );
          table.ForeignKey(
            name: "FK_UserCartItems_UserCarts_CartId",
            column: x => x.CartId,
            principalTable: "UserCarts",
            principalColumn: "CartId",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "WishlistProducts",
        columns: table => new
        {
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          WishlistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_WishlistProducts", x => new { x.ProductId, x.WishlistId });
          table.ForeignKey(
            name: "FK_WishlistProducts_Products_ProductId1",
            column: x => x.ProductId1,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
          );
          table.ForeignKey(
            name: "FK_WishlistProducts_Wishlists_WishlistId",
            column: x => x.WishlistId,
            principalTable: "Wishlists",
            principalColumn: "WishlistId",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_Products_SellerId",
        table: "Products",
        column: "SellerId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Notifications_UserId",
        table: "Notifications",
        column: "UserId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_OrderItems_OrderId",
        table: "OrderItems",
        column: "OrderId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_OrderItems_ProductId",
        table: "OrderItems",
        column: "ProductId"
      );

      migrationBuilder.CreateIndex(name: "IX_Orders_UserId", table: "Orders", column: "UserId");

      migrationBuilder.CreateIndex(
        name: "IX_UserCartItems_CartId",
        table: "UserCartItems",
        column: "CartId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_UserCartItems_ProductId",
        table: "UserCartItems",
        column: "ProductId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_UserCarts_UserId",
        table: "UserCarts",
        column: "UserId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_WishlistProducts_ProductId1",
        table: "WishlistProducts",
        column: "ProductId1"
      );

      migrationBuilder.CreateIndex(
        name: "IX_WishlistProducts_WishlistId",
        table: "WishlistProducts",
        column: "WishlistId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Wishlists_UserId",
        table: "Wishlists",
        column: "UserId",
        unique: true
      );

      migrationBuilder.AddForeignKey(
        name: "FK_Products_Users_SellerId",
        table: "Products",
        column: "SellerId",
        principalTable: "Users",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(name: "FK_Products_Users_SellerId", table: "Products");

      migrationBuilder.DropTable(name: "Notifications");

      migrationBuilder.DropTable(name: "OrderItems");

      migrationBuilder.DropTable(name: "UserCartItems");

      migrationBuilder.DropTable(name: "WishlistProducts");

      migrationBuilder.DropTable(name: "Orders");

      migrationBuilder.DropTable(name: "UserCarts");

      migrationBuilder.DropTable(name: "Wishlists");

      migrationBuilder.DropIndex(name: "IX_Products_SellerId", table: "Products");

      migrationBuilder.DropColumn(name: "Address_Country", table: "Users");

      migrationBuilder.DropColumn(name: "City", table: "Users");

      migrationBuilder.DropColumn(name: "CountryCode", table: "Users");

      migrationBuilder.DropColumn(name: "State", table: "Users");

      migrationBuilder.DropColumn(name: "Street", table: "Users");

      migrationBuilder.DropColumn(name: "ZipCode", table: "Users");
    }
  }
}
