using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class pendingmodelchanges : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(
        name: "IX_ProductImages_BackImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_BottomImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_FrontImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_LeftImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_RightImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_TopImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "Wishlists",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "WishlistId",
        table: "Wishlists",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId1",
        table: "WishlistProducts",
        type: "uuid",
        nullable: true,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "WishlistId",
        table: "WishlistProducts",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "WishlistProducts",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<string>(
        name: "PhoneNumber",
        table: "Users",
        type: "character varying(50)",
        maxLength: 50,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50
      );

      migrationBuilder.AlterColumn<string>(
        name: "PasswordResetOtp_Value",
        table: "Users",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "PasswordResetOtp_Expiry",
        table: "Users",
        type: "timestamp with time zone",
        nullable: true,
        oldClrType: typeof(DateTime),
        oldType: "datetime2",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Password",
        table: "Users",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "LastName",
        table: "Users",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "FirstName",
        table: "Users",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "EmailVerificationOtp_Value",
        table: "Users",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "EmailVerificationOtp_Expiry",
        table: "Users",
        type: "timestamp with time zone",
        nullable: true,
        oldClrType: typeof(DateTime),
        oldType: "datetime2",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Email",
        table: "Users",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "CountryCode",
        table: "Users",
        type: "character varying(3)",
        maxLength: 3,
        nullable: false,
        defaultValue: "251",
        oldClrType: typeof(string),
        oldType: "nvarchar(3)",
        oldMaxLength: 3,
        oldDefaultValue: "251"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_ZipCode",
        table: "Users",
        type: "character varying(10)",
        maxLength: 10,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(10)",
        oldMaxLength: 10,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Street",
        table: "Users",
        type: "character varying(255)",
        maxLength: 255,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_State",
        table: "Users",
        type: "character varying(100)",
        maxLength: 100,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Country",
        table: "Users",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_City",
        table: "Users",
        type: "character varying(100)",
        maxLength: 100,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "Users",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "UserCarts",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "CartId",
        table: "UserCarts",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<int>(
        name: "Quantity",
        table: "UserCartItems",
        type: "integer",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "int"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "UserCartItems",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "CartId",
        table: "UserCartItems",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "CartItemId",
        table: "UserCartItems",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<string>(
        name: "TagName",
        table: "ProductTags",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductTags",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "TagId",
        table: "ProductTags",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<int>(
        name: "StockQuantity",
        table: "Products",
        type: "integer",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "int"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "SellerId",
        table: "Products",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "PriceValue",
        table: "Products",
        type: "numeric",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric(18,2)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Name",
        table: "Products",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Products",
        type: "character varying(1000)",
        maxLength: 1000,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(1000)",
        oldMaxLength: 1000
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "AverageRating",
        table: "Products",
        type: "numeric",
        nullable: false,
        defaultValue: 0m,
        oldClrType: typeof(decimal),
        oldType: "numeric(18,2)",
        oldDefaultValue: 0m
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "Products",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "Rating",
        table: "ProductReviews",
        type: "numeric",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric(18,2)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "ProductReviews",
        type: "character varying(1000)",
        maxLength: 1000,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(1000)",
        oldMaxLength: 1000
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "AuthorId",
        table: "ProductReviews",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductReviews",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ReviewId",
        table: "ProductReviews",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "StartDate",
        table: "ProductPromotions",
        type: "timestamp with time zone",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "datetime2"
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "EndDate",
        table: "ProductPromotions",
        type: "timestamp with time zone",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "datetime2"
      );

      migrationBuilder.AlterColumn<int>(
        name: "DiscountPercentage",
        table: "ProductPromotions",
        type: "integer",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "int"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductPromotions",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductPromotionId",
        table: "ProductPromotions",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<string>(
        name: "TopImage_Url",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "TopImage_ObjectIdentifier",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Thumbnail_Url",
        table: "ProductImages",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Thumbnail_ObjectIdentifier",
        table: "ProductImages",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "RightImage_Url",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "RightImage_ObjectIdentifier",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "LeftImage_Url",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "LeftImage_ObjectIdentifier",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "FrontImage_Url",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "FrontImage_ObjectIdentifier",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BottomImage_Url",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BottomImage_ObjectIdentifier",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BackImage_Url",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BackImage_ObjectIdentifier",
        table: "ProductImages",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductImages",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductImageId",
        table: "ProductImages",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductCategoryLink",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductCategoryId",
        table: "ProductCategoryLink",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductCategories",
        type: "uuid",
        nullable: true,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Name",
        table: "ProductCategories",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "ProductCategories",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "Orders",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "TotalPrice",
        table: "Orders",
        type: "numeric",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric(18,2)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Status",
        table: "Orders",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingZipCode",
        table: "Orders",
        type: "character varying(10)",
        maxLength: 10,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(10)",
        oldMaxLength: 10
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingStreet",
        table: "Orders",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingState",
        table: "Orders",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingCountry",
        table: "Orders",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingCity",
        table: "Orders",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "OrderDate",
        table: "Orders",
        type: "timestamp with time zone",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "datetime2"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Currency",
        table: "Orders",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CardNumber",
        table: "Orders",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CardHolderName",
        table: "Orders",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<int>(
        name: "CardExpiryYear",
        table: "Orders",
        type: "integer",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "int"
      );

      migrationBuilder.AlterColumn<int>(
        name: "CardExpiryMonth",
        table: "Orders",
        type: "integer",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "int"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CardCVV",
        table: "Orders",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingZipCode",
        table: "Orders",
        type: "character varying(10)",
        maxLength: 10,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(10)",
        oldMaxLength: 10
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingStreet",
        table: "Orders",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingState",
        table: "Orders",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingCountry",
        table: "Orders",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingCity",
        table: "Orders",
        type: "character varying(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "OrderId",
        table: "Orders",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<int>(
        name: "Quantity",
        table: "OrderItems",
        type: "integer",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "int"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "OrderItems",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "PriceValue",
        table: "OrderItems",
        type: "numeric",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric(18,2)"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "OrderId",
        table: "OrderItems",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "OrderItemId",
        table: "OrderItems",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "Notifications",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Title",
        table: "Notifications",
        type: "character varying(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "ReadAt",
        table: "Notifications",
        type: "timestamp with time zone",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "datetime2"
      );

      migrationBuilder.AlterColumn<string>(
        name: "NotificationType",
        table: "Notifications",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "NotificationStatus",
        table: "Notifications",
        type: "text",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Notifications",
        type: "character varying(1000)",
        maxLength: 1000,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(1000)",
        oldMaxLength: 1000
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "NotificationId",
        table: "Notifications",
        type: "uuid",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_BackImage_ObjectIdentifier",
        table: "ProductImages",
        column: "BackImage_ObjectIdentifier",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_BottomImage_ObjectIdentifier",
        table: "ProductImages",
        column: "BottomImage_ObjectIdentifier",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_FrontImage_ObjectIdentifier",
        table: "ProductImages",
        column: "FrontImage_ObjectIdentifier",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_LeftImage_ObjectIdentifier",
        table: "ProductImages",
        column: "LeftImage_ObjectIdentifier",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_RightImage_ObjectIdentifier",
        table: "ProductImages",
        column: "RightImage_ObjectIdentifier",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_TopImage_ObjectIdentifier",
        table: "ProductImages",
        column: "TopImage_ObjectIdentifier",
        unique: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(
        name: "IX_ProductImages_BackImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_BottomImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_FrontImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_LeftImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_RightImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.DropIndex(
        name: "IX_ProductImages_TopImage_ObjectIdentifier",
        table: "ProductImages"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "Wishlists",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "WishlistId",
        table: "Wishlists",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId1",
        table: "WishlistProducts",
        type: "uniqueidentifier",
        nullable: true,
        oldClrType: typeof(Guid),
        oldType: "uuid",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "WishlistId",
        table: "WishlistProducts",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "WishlistProducts",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<string>(
        name: "PhoneNumber",
        table: "Users",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(50)",
        oldMaxLength: 50
      );

      migrationBuilder.AlterColumn<string>(
        name: "PasswordResetOtp_Value",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "PasswordResetOtp_Expiry",
        table: "Users",
        type: "datetime2",
        nullable: true,
        oldClrType: typeof(DateTime),
        oldType: "timestamp with time zone",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Password",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "LastName",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "FirstName",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "EmailVerificationOtp_Value",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "EmailVerificationOtp_Expiry",
        table: "Users",
        type: "datetime2",
        nullable: true,
        oldClrType: typeof(DateTime),
        oldType: "timestamp with time zone",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Email",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "CountryCode",
        table: "Users",
        type: "nvarchar(3)",
        maxLength: 3,
        nullable: false,
        defaultValue: "251",
        oldClrType: typeof(string),
        oldType: "character varying(3)",
        oldMaxLength: 3,
        oldDefaultValue: "251"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_ZipCode",
        table: "Users",
        type: "nvarchar(10)",
        maxLength: 10,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "character varying(10)",
        oldMaxLength: 10,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Street",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_State",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Country",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_City",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "Users",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "UserCarts",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "CartId",
        table: "UserCarts",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<int>(
        name: "Quantity",
        table: "UserCartItems",
        type: "int",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "UserCartItems",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "CartId",
        table: "UserCartItems",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "CartItemId",
        table: "UserCartItems",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<string>(
        name: "TagName",
        table: "ProductTags",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductTags",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "TagId",
        table: "ProductTags",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<int>(
        name: "StockQuantity",
        table: "Products",
        type: "int",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "SellerId",
        table: "Products",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "PriceValue",
        table: "Products",
        type: "numeric(18,2)",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Name",
        table: "Products",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Products",
        type: "nvarchar(1000)",
        maxLength: 1000,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(1000)",
        oldMaxLength: 1000
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "AverageRating",
        table: "Products",
        type: "numeric(18,2)",
        nullable: false,
        defaultValue: 0m,
        oldClrType: typeof(decimal),
        oldType: "numeric",
        oldDefaultValue: 0m
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "Products",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "Rating",
        table: "ProductReviews",
        type: "numeric(18,2)",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "ProductReviews",
        type: "nvarchar(1000)",
        maxLength: 1000,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(1000)",
        oldMaxLength: 1000
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "AuthorId",
        table: "ProductReviews",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductReviews",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ReviewId",
        table: "ProductReviews",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "StartDate",
        table: "ProductPromotions",
        type: "datetime2",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "timestamp with time zone"
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "EndDate",
        table: "ProductPromotions",
        type: "datetime2",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "timestamp with time zone"
      );

      migrationBuilder.AlterColumn<int>(
        name: "DiscountPercentage",
        table: "ProductPromotions",
        type: "int",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductPromotions",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductPromotionId",
        table: "ProductPromotions",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<string>(
        name: "TopImage_Url",
        table: "ProductImages",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "TopImage_ObjectIdentifier",
        table: "ProductImages",
        type: "nvarchar(450)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Thumbnail_Url",
        table: "ProductImages",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Thumbnail_ObjectIdentifier",
        table: "ProductImages",
        type: "nvarchar(450)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "RightImage_Url",
        table: "ProductImages",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "RightImage_ObjectIdentifier",
        table: "ProductImages",
        type: "nvarchar(450)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "LeftImage_Url",
        table: "ProductImages",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "LeftImage_ObjectIdentifier",
        table: "ProductImages",
        type: "nvarchar(450)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "FrontImage_Url",
        table: "ProductImages",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "FrontImage_ObjectIdentifier",
        table: "ProductImages",
        type: "nvarchar(450)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BottomImage_Url",
        table: "ProductImages",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BottomImage_ObjectIdentifier",
        table: "ProductImages",
        type: "nvarchar(450)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BackImage_Url",
        table: "ProductImages",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "BackImage_ObjectIdentifier",
        table: "ProductImages",
        type: "nvarchar(450)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductImages",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductImageId",
        table: "ProductImages",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductCategoryLink",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductCategoryId",
        table: "ProductCategoryLink",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductCategories",
        type: "uniqueidentifier",
        nullable: true,
        oldClrType: typeof(Guid),
        oldType: "uuid",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Name",
        table: "ProductCategories",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "ProductCategories",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "Orders",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "TotalPrice",
        table: "Orders",
        type: "numeric(18,2)",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Status",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingZipCode",
        table: "Orders",
        type: "nvarchar(10)",
        maxLength: 10,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(10)",
        oldMaxLength: 10
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingStreet",
        table: "Orders",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingState",
        table: "Orders",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingCountry",
        table: "Orders",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "ShippingCity",
        table: "Orders",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "OrderDate",
        table: "Orders",
        type: "datetime2",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "timestamp with time zone"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Currency",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CardNumber",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CardHolderName",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<int>(
        name: "CardExpiryYear",
        table: "Orders",
        type: "int",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AlterColumn<int>(
        name: "CardExpiryMonth",
        table: "Orders",
        type: "int",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AlterColumn<string>(
        name: "CardCVV",
        table: "Orders",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingZipCode",
        table: "Orders",
        type: "nvarchar(10)",
        maxLength: 10,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(10)",
        oldMaxLength: 10
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingStreet",
        table: "Orders",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingState",
        table: "Orders",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingCountry",
        table: "Orders",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "BillingCity",
        table: "Orders",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "OrderId",
        table: "Orders",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<int>(
        name: "Quantity",
        table: "OrderItems",
        type: "int",
        nullable: false,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "OrderItems",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<decimal>(
        name: "PriceValue",
        table: "OrderItems",
        type: "numeric(18,2)",
        nullable: false,
        oldClrType: typeof(decimal),
        oldType: "numeric"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "OrderId",
        table: "OrderItems",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "OrderItemId",
        table: "OrderItems",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "UserId",
        table: "Notifications",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Title",
        table: "Notifications",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<DateTime>(
        name: "ReadAt",
        table: "Notifications",
        type: "datetime2",
        nullable: false,
        oldClrType: typeof(DateTime),
        oldType: "timestamp with time zone"
      );

      migrationBuilder.AlterColumn<string>(
        name: "NotificationType",
        table: "Notifications",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "NotificationStatus",
        table: "Notifications",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Notifications",
        type: "nvarchar(1000)",
        maxLength: 1000,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(1000)",
        oldMaxLength: 1000
      );

      migrationBuilder.AlterColumn<Guid>(
        name: "NotificationId",
        table: "Notifications",
        type: "uniqueidentifier",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uuid"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_BackImage_ObjectIdentifier",
        table: "ProductImages",
        column: "BackImage_ObjectIdentifier",
        unique: true,
        filter: "[BackImage_ObjectIdentifier] IS NOT NULL"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_BottomImage_ObjectIdentifier",
        table: "ProductImages",
        column: "BottomImage_ObjectIdentifier",
        unique: true,
        filter: "[BottomImage_ObjectIdentifier] IS NOT NULL"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_FrontImage_ObjectIdentifier",
        table: "ProductImages",
        column: "FrontImage_ObjectIdentifier",
        unique: true,
        filter: "[FrontImage_ObjectIdentifier] IS NOT NULL"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_LeftImage_ObjectIdentifier",
        table: "ProductImages",
        column: "LeftImage_ObjectIdentifier",
        unique: true,
        filter: "[LeftImage_ObjectIdentifier] IS NOT NULL"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_RightImage_ObjectIdentifier",
        table: "ProductImages",
        column: "RightImage_ObjectIdentifier",
        unique: true,
        filter: "[RightImage_ObjectIdentifier] IS NOT NULL"
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductImages_TopImage_ObjectIdentifier",
        table: "ProductImages",
        column: "TopImage_ObjectIdentifier",
        unique: true,
        filter: "[TopImage_ObjectIdentifier] IS NOT NULL"
      );
    }
  }
}
