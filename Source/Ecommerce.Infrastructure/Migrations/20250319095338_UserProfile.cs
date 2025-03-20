using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class UserProfile : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(
        name: "AuthorId",
        table: "ProductReviews",
        newName: "ReviewerId"
      );

      migrationBuilder.AddColumn<string>(
        name: "ProfileImage_ObjectIdentifier",
        table: "Users",
        type: "nvarchar(450)",
        nullable: true
      );

      migrationBuilder.AddColumn<string>(
        name: "ProfileImage_Url",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_Users_ProfileImage_ObjectIdentifier",
        table: "Users",
        column: "ProfileImage_ObjectIdentifier",
        unique: true,
        filter: "[ProfileImage_ObjectIdentifier] IS NOT NULL"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(name: "IX_Users_ProfileImage_ObjectIdentifier", table: "Users");

      migrationBuilder.DropColumn(name: "ProfileImage_ObjectIdentifier", table: "Users");

      migrationBuilder.DropColumn(name: "ProfileImage_Url", table: "Users");

      migrationBuilder.RenameColumn(
        name: "ReviewerId",
        table: "ProductReviews",
        newName: "AuthorId"
      );
    }
  }
}
