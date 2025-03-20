using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class Seed0 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
        table: "Users",
        columns: new[]
        {
          "Id",
          "AccountVerified",
          "Email",
          "FirstName",
          "LastName",
          "Password",
          "PhoneNumber",
        },
        values: new object[]
        {
          new Guid("c49b9246-e6ba-45b7-a9b9-f302f21eed4d"),
          true,
          "johndoe@example.com",
          "John",
          "Doe",
          "SecurePassword123!",
          "+1234567890",
        }
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DeleteData(
        table: "Users",
        keyColumn: "Id",
        keyValue: new Guid("c49b9246-e6ba-45b7-a9b9-f302f21eed4d")
      );
    }
  }
}
