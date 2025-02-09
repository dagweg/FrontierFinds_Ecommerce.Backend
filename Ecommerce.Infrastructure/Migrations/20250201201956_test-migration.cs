using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class testmigration : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "Address_ZipCode",
        table: "Users",
        type: "nvarchar(10)",
        maxLength: 10,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(10)",
        oldMaxLength: 10
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Street",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_State",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Country",
        table: "Users",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_City",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "Address_ZipCode",
        table: "Users",
        type: "nvarchar(10)",
        maxLength: 10,
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(10)",
        oldMaxLength: 10,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Street",
        table: "Users",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_State",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100,
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_Country",
        table: "Users",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Address_City",
        table: "Users",
        type: "nvarchar(100)",
        maxLength: 100,
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(100)",
        oldMaxLength: 100,
        oldNullable: true
      );
    }
  }
}
