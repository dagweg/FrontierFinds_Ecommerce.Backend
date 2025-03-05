using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class UserProperties : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(name: "ZipCode", table: "Users", newName: "Address_ZipCode");

      migrationBuilder.RenameColumn(name: "Street", table: "Users", newName: "Address_Street");

      migrationBuilder.RenameColumn(name: "State", table: "Users", newName: "Address_State");

      migrationBuilder.RenameColumn(name: "City", table: "Users", newName: "Address_City");

      migrationBuilder.AlterColumn<string>(
        name: "PhoneNumber",
        table: "Users",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(15)",
        oldMaxLength: 15
      );

      migrationBuilder.AlterColumn<string>(
        name: "Password",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(512)",
        oldMaxLength: 512
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(name: "Address_ZipCode", table: "Users", newName: "ZipCode");

      migrationBuilder.RenameColumn(name: "Address_Street", table: "Users", newName: "Street");

      migrationBuilder.RenameColumn(name: "Address_State", table: "Users", newName: "State");

      migrationBuilder.RenameColumn(name: "Address_City", table: "Users", newName: "City");

      migrationBuilder.AlterColumn<string>(
        name: "PhoneNumber",
        table: "Users",
        type: "nvarchar(15)",
        maxLength: 15,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50
      );

      migrationBuilder.AlterColumn<string>(
        name: "Password",
        table: "Users",
        type: "nvarchar(512)",
        maxLength: 512,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );
    }
  }
}
