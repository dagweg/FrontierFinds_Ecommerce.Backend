using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedImageFromEntityToValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "BackImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "BottomImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "FrontImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "LeftImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "RightImage_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "Thumbnail_Id", table: "ProductImages");

            migrationBuilder.DropColumn(name: "TopImage_Id", table: "ProductImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
              name: "BackImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "BottomImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "FrontImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "LeftImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "RightImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );

            migrationBuilder.AddColumn<Guid>(
              name: "Thumbnail_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: false,
              defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.AddColumn<Guid>(
              name: "TopImage_Id",
              table: "ProductImages",
              type: "uniqueidentifier",
              nullable: true
            );
        }
    }
}
