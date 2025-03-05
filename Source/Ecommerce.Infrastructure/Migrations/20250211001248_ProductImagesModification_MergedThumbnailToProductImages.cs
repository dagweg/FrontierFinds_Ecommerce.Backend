using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductImagesModification_MergedThumbnailToProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ProductThumbnails");

            migrationBuilder.RenameColumn(
              name: "RightImage",
              table: "ProductImages",
              newName: "TopImage_Url"
            );

            migrationBuilder.RenameColumn(
              name: "LeftImage",
              table: "ProductImages",
              newName: "TopImage_ProductView"
            );

            migrationBuilder.RenameColumn(
              name: "FrontImage",
              table: "ProductImages",
              newName: "RightImage_Url"
            );

            migrationBuilder.RenameColumn(
              name: "BackImage",
              table: "ProductImages",
              newName: "RightImage_ProductView"
            );

            migrationBuilder.AddColumn<Guid>(
              name: "BackImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "BackImage_ObjectIdentifier",
              table: "ProductImages",
              type: "nvarchar(450)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "BackImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "BackImage_Url",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "BottomImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "BottomImage_ObjectIdentifier",
              table: "ProductImages",
              type: "nvarchar(450)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "BottomImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "BottomImage_Url",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "FrontImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "FrontImage_ObjectIdentifier",
              table: "ProductImages",
              type: "nvarchar(450)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "FrontImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "FrontImage_Url",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "LeftImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "LeftImage_ObjectIdentifier",
              table: "ProductImages",
              type: "nvarchar(450)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "LeftImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "LeftImage_Url",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "RightImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "RightImage_ObjectIdentifier",
              table: "ProductImages",
              type: "nvarchar(450)",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "Thumbnail_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: false,
              defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.AddColumn<string>(
              name: "Thumbnail_ObjectIdentifier",
              table: "ProductImages",
              type: "nvarchar(450)",
              nullable: false,
              defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
              name: "Thumbnail_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: false,
              defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
              name: "Thumbnail_Url",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: false,
              defaultValue: ""
            );

            migrationBuilder.AddColumn<Guid>(
              name: "TopImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "TopImage_ObjectIdentifier",
              table: "ProductImages",
              type: "nvarchar(450)",
              nullable: true
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
              name: "IX_ProductImages_Thumbnail_ObjectIdentifier",
              table: "ProductImages",
              column: "Thumbnail_ObjectIdentifier",
              unique: true
            );

            migrationBuilder.CreateIndex(
              name: "IX_ProductImages_TopImage_ObjectIdentifier",
              table: "ProductImages",
              column: "TopImage_ObjectIdentifier",
              unique: true,
              filter: "[TopImage_ObjectIdentifier] IS NOT NULL"
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
              name: "IX_ProductImages_Thumbnail_ObjectIdentifier",
              table: "ProductImages"
            );

            migrationBuilder.DropIndex(
              name: "IX_ProductImages_TopImage_ObjectIdentifier",
              table: "ProductImages"
            );

            migrationBuilder.DropColumn(name: "BackImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BackImage_ObjectIdentifier", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BackImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BackImage_Url", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BottomImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BottomImage_ObjectIdentifier", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BottomImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BottomImage_Url", table: "ProductImages");

            migrationBuilder.DropColumn(name: "FrontImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "FrontImage_ObjectIdentifier", table: "ProductImages");

            migrationBuilder.DropColumn(name: "FrontImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "FrontImage_Url", table: "ProductImages");

            migrationBuilder.DropColumn(name: "LeftImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "LeftImage_ObjectIdentifier", table: "ProductImages");

            migrationBuilder.DropColumn(name: "LeftImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "LeftImage_Url", table: "ProductImages");

            migrationBuilder.DropColumn(name: "RightImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "RightImage_ObjectIdentifier", table: "ProductImages");

            migrationBuilder.DropColumn(name: "Thumbnail_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "Thumbnail_ObjectIdentifier", table: "ProductImages");

            migrationBuilder.DropColumn(name: "Thumbnail_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "Thumbnail_Url", table: "ProductImages");

            migrationBuilder.DropColumn(name: "TopImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "TopImage_ObjectIdentifier", table: "ProductImages");

            migrationBuilder.RenameColumn(
              name: "TopImage_Url",
              table: "ProductImages",
              newName: "RightImage"
            );

            migrationBuilder.RenameColumn(
              name: "TopImage_ProductView",
              table: "ProductImages",
              newName: "LeftImage"
            );

            migrationBuilder.RenameColumn(
              name: "RightImage_Url",
              table: "ProductImages",
              newName: "FrontImage"
            );

            migrationBuilder.RenameColumn(
              name: "RightImage_ProductView",
              table: "ProductImages",
              newName: "BackImage"
            );

            migrationBuilder.CreateTable(
              name: "ProductThumbnails",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
              name: "IX_ProductThumbnails_ProductId",
              table: "ProductThumbnails",
              column: "ProductId",
              unique: true
            );
        }
    }
}
