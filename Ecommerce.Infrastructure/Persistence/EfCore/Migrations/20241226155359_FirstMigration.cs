using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class FirstMigration : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
          LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
          PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Users", x => x.Id);
        }
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Users");
    }
  }
}
