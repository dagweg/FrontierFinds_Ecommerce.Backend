using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class TestMigration : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "PhoneNumber",
        table: "Users",
        type: "nvarchar(15)",
        maxLength: 15,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Password",
        table: "Users",
        type: "nvarchar(512)",
        maxLength: 512,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "LastName",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "FirstName",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Email",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.CreateTable(
        name: "Products",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
          Description = table.Column<string>(
            type: "nvarchar(1000)",
            maxLength: 1000,
            nullable: false
          ),
          PriceValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
          PriceCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
          StockQuantity = table.Column<int>(type: "int", nullable: false),
          StockReserved = table.Column<int>(type: "int", nullable: false),
          SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          AverageRating = table.Column<decimal>(
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m
          ),
          CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
          UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Products", x => x.Id);
        }
      );

      migrationBuilder.CreateTable(
        name: "ProductCategories",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductCategories", x => x.Id);
          table.ForeignKey(
            name: "FK_ProductCategories_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "ProductImages",
        columns: table => new
        {
          ProductImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          LeftImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
          RightImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
          FrontImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
          BackImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductImages", x => new { x.ProductImageId, x.ProductId });
          table.ForeignKey(
            name: "FK_ProductImages_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "ProductPromotions",
        columns: table => new
        {
          ProductPromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          DiscountPercentage = table.Column<int>(type: "int", nullable: false),
          StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
          EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductPromotions", x => new { x.ProductPromotionId, x.ProductId });
          table.ForeignKey(
            name: "FK_ProductPromotions_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "ProductReviews",
        columns: table => new
        {
          ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Description = table.Column<string>(
            type: "nvarchar(1000)",
            maxLength: 1000,
            nullable: false
          ),
          Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductReviews", x => new { x.ReviewId, x.ProductId });
          table.ForeignKey(
            name: "FK_ProductReviews_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "ProductTags",
        columns: table => new
        {
          TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          TagName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductTags", x => new { x.TagId, x.ProductId });
          table.ForeignKey(
            name: "FK_ProductTags_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "ProductThumbnails",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
          FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
          FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
          FileSize = table.Column<long>(type: "bigint", nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductThumbnails", x => new { x.Id, x.ProductId });
          table.ForeignKey(
            name: "FK_ProductThumbnails_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "ProductCategoryLink",
        columns: table => new
        {
          ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductCategoryLink", x => new { x.ProductCategoryId, x.ProductId });
          table.ForeignKey(
            name: "FK_ProductCategoryLink_ProductCategories_ProductCategoryId",
            column: x => x.ProductCategoryId,
            principalTable: "ProductCategories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
          table.ForeignKey(
            name: "FK_ProductCategoryLink_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductCategories_ProductId",
        table: "ProductCategories",
        column: "ProductId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductCategoryLink_ProductId",
        table: "ProductCategoryLink",
        column: "ProductId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_ProductId",
        table: "ProductImages",
        column: "ProductId",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductPromotions_ProductId",
        table: "ProductPromotions",
        column: "ProductId",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductReviews_ProductId",
        table: "ProductReviews",
        column: "ProductId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductTags_ProductId",
        table: "ProductTags",
        column: "ProductId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductThumbnails_ProductId",
        table: "ProductThumbnails",
        column: "ProductId",
        unique: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "ProductCategoryLink");

      migrationBuilder.DropTable(name: "ProductImages");

      migrationBuilder.DropTable(name: "ProductPromotions");

      migrationBuilder.DropTable(name: "ProductReviews");

      migrationBuilder.DropTable(name: "ProductTags");

      migrationBuilder.DropTable(name: "ProductThumbnails");

      migrationBuilder.DropTable(name: "ProductCategories");

      migrationBuilder.DropTable(name: "Products");

      migrationBuilder.AlterColumn<string>(
        name: "PhoneNumber",
        table: "Users",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(15)",
        oldMaxLength: 15
      );

      migrationBuilder.AlterColumn<string>(
        name: "Password",
        table: "Users",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(512)",
        oldMaxLength: 512
      );

      migrationBuilder.AlterColumn<string>(
        name: "LastName",
        table: "Users",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "FirstName",
        table: "Users",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "Email",
        table: "Users",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );
    }
  }
}
