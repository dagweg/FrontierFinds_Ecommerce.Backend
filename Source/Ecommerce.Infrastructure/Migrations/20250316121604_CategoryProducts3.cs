using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class CategoryProducts3 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "IsActive",
        table: "Categories",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: "True",
        oldClrType: typeof(bool),
        oldType: "bit",
        oldDefaultValue: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<bool>(
        name: "IsActive",
        table: "Categories",
        type: "bit",
        nullable: false,
        defaultValue: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldDefaultValue: "True"
      );
    }
  }
}
