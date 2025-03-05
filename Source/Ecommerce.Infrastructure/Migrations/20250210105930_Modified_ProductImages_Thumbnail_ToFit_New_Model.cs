using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modified_ProductImages_Thumbnail_ToFit_New_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "FileName", table: "ProductThumbnails");

            migrationBuilder.DropColumn(name: "FileSize", table: "ProductThumbnails");

            migrationBuilder.DropColumn(name: "FileType", table: "ProductThumbnails");

            migrationBuilder.DropColumn(name: "StockReserved", table: "Products");

            migrationBuilder.RenameColumn(
              name: "RightImageUrl",
              table: "ProductImages",
              newName: "RightImage"
            );

            migrationBuilder.RenameColumn(
              name: "LeftImageUrl",
              table: "ProductImages",
              newName: "LeftImage"
            );

            migrationBuilder.RenameColumn(
              name: "FrontImageUrl",
              table: "ProductImages",
              newName: "FrontImage"
            );

            migrationBuilder.RenameColumn(
              name: "BackImageUrl",
              table: "ProductImages",
              newName: "BackImage"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
              name: "RightImage",
              table: "ProductImages",
              newName: "RightImageUrl"
            );

            migrationBuilder.RenameColumn(
              name: "LeftImage",
              table: "ProductImages",
              newName: "LeftImageUrl"
            );

            migrationBuilder.RenameColumn(
              name: "FrontImage",
              table: "ProductImages",
              newName: "FrontImageUrl"
            );

            migrationBuilder.RenameColumn(
              name: "BackImage",
              table: "ProductImages",
              newName: "BackImageUrl"
            );

            migrationBuilder.AddColumn<string>(
              name: "FileName",
              table: "ProductThumbnails",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<long>(
              name: "FileSize",
              table: "ProductThumbnails",
              type: "bigint",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "FileType",
              table: "ProductThumbnails",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<int>(
              name: "StockReserved",
              table: "Products",
              type: "int",
              nullable: false,
              defaultValue: 0
            );
        }
    }
}
