using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedColumnNaming_RemovedUnnecessaryFieldProductView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "BackImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BottomImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "FrontImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "LeftImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "RightImage_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "Thumbnail_ProductView", table: "ProductImages");

            migrationBuilder.DropColumn(name: "TopImage_ProductView", table: "ProductImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
              name: "BackImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "BottomImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "FrontImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "LeftImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "RightImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "Thumbnail_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: false,
              defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
              name: "TopImage_ProductView",
              table: "ProductImages",
              type: "nvarchar(max)",
              nullable: true
            );
        }
    }
}
