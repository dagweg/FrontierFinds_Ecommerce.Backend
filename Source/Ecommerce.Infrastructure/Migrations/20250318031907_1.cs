using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class _1 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_ProductTags_Products_ProductId",
        table: "ProductTags"
      );

      migrationBuilder.DropPrimaryKey(name: "PK_ProductTags", table: "ProductTags");

      migrationBuilder.DropIndex(name: "IX_ProductTags_ProductId", table: "ProductTags");

      migrationBuilder.DropColumn(name: "ProductId", table: "ProductTags");

      migrationBuilder.RenameColumn(name: "TagName", table: "ProductTags", newName: "Name");

      migrationBuilder.RenameColumn(name: "TagId", table: "ProductTags", newName: "Id");

      migrationBuilder.AlterColumn<string>(
        name: "Name",
        table: "ProductTags",
        type: "nvarchar(450)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AddPrimaryKey(name: "PK_ProductTags", table: "ProductTags", column: "Id");

      migrationBuilder.CreateTable(
        name: "ProductTagLink",
        columns: table => new
        {
          ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ProductTagLink", x => new { x.ProductsId, x.TagsId });
          table.ForeignKey(
            name: "FK_ProductTagLink_ProductTags_TagsId",
            column: x => x.TagsId,
            principalTable: "ProductTags",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
          table.ForeignKey(
            name: "FK_ProductTagLink_Products_ProductsId",
            column: x => x.ProductsId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductTags_Name",
        table: "ProductTags",
        column: "Name",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductTagLink_TagsId",
        table: "ProductTagLink",
        column: "TagsId"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "ProductTagLink");

      migrationBuilder.DropPrimaryKey(name: "PK_ProductTags", table: "ProductTags");

      migrationBuilder.DropIndex(name: "IX_ProductTags_Name", table: "ProductTags");

      migrationBuilder.RenameColumn(name: "Name", table: "ProductTags", newName: "TagName");

      migrationBuilder.RenameColumn(name: "Id", table: "ProductTags", newName: "TagId");

      migrationBuilder.AlterColumn<string>(
        name: "TagName",
        table: "ProductTags",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(450)"
      );

      migrationBuilder.AddColumn<Guid>(
        name: "ProductId",
        table: "ProductTags",
        type: "uniqueidentifier",
        nullable: false,
        defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
      );

      migrationBuilder.AddPrimaryKey(
        name: "PK_ProductTags",
        table: "ProductTags",
        columns: new[] { "TagId", "ProductId" }
      );

      migrationBuilder.CreateIndex(
        name: "IX_ProductTags_ProductId",
        table: "ProductTags",
        column: "ProductId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_ProductTags_Products_ProductId",
        table: "ProductTags",
        column: "ProductId",
        principalTable: "Products",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );
    }
  }
}
