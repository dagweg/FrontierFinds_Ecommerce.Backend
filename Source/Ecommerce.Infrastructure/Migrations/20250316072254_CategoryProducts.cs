using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class CategoryProducts : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "ProductCategoryLink");

      migrationBuilder.DropPrimaryKey(name: "PK_ProductCategories", table: "ProductCategories");

      migrationBuilder.DropIndex(
        name: "IX_ProductCategories_ProductId",
        table: "ProductCategories"
      );

      migrationBuilder.DeleteData(
        table: "Users",
        keyColumn: "Id",
        keyValue: new Guid("c49b9246-e6ba-45b7-a9b9-f302f21eed4d")
      );

      migrationBuilder.DropColumn(name: "Name", table: "ProductCategories");

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductCategories",
        type: "uniqueidentifier",
        nullable: false,
        defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier",
        oldNullable: true
      );

      migrationBuilder.AddColumn<int>(
        name: "CategoryId",
        table: "ProductCategories",
        type: "int",
        nullable: false,
        defaultValue: 0
      );

      migrationBuilder.AddPrimaryKey(
        name: "PK_ProductCategories",
        table: "ProductCategories",
        columns: new[] { "ProductId", "CategoryId" }
      );

      migrationBuilder.CreateTable(
        name: "Categories",
        columns: table => new
        {
          Id = table
            .Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
          ParentId = table.Column<int>(type: "int", nullable: true),
          IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Categories", x => x.Id);
          table.ForeignKey(
            name: "FK_Categories_Categories_ParentId",
            column: x => x.ParentId,
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductCategories_CategoryId",
        table: "ProductCategories",
        column: "CategoryId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Categories_ParentId",
        table: "Categories",
        column: "ParentId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_ProductCategories_Categories_CategoryId",
        table: "ProductCategories",
        column: "CategoryId",
        principalTable: "Categories",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_ProductCategories_Categories_CategoryId",
        table: "ProductCategories"
      );

      migrationBuilder.DropTable(name: "Categories");

      migrationBuilder.DropPrimaryKey(name: "PK_ProductCategories", table: "ProductCategories");

      migrationBuilder.DropIndex(
        name: "IX_ProductCategories_CategoryId",
        table: "ProductCategories"
      );

      migrationBuilder.DropColumn(name: "CategoryId", table: "ProductCategories");

      migrationBuilder.AlterColumn<Guid>(
        name: "ProductId",
        table: "ProductCategories",
        type: "uniqueidentifier",
        nullable: true,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier"
      );

      migrationBuilder.AddColumn<string>(
        name: "Name",
        table: "ProductCategories",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddPrimaryKey(
        name: "PK_ProductCategories",
        table: "ProductCategories",
        column: "Id"
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

      migrationBuilder.InsertData(
        table: "Users",
        columns: new[]
        {
          "Id",
          "AccountVerified",
          "Email",
          "FirstName",
          "LastName",
          "Password",
          "PhoneNumber",
        },
        values: new object[]
        {
          new Guid("c49b9246-e6ba-45b7-a9b9-f302f21eed4d"),
          true,
          "johndoe@example.com",
          "John",
          "Doe",
          "SecurePassword123!",
          "+1234567890",
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
    }
  }
}
