using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class otpnextresendvalidatprop : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<bool>(
        name: "AccountVerified",
        table: "Users",
        type: "bit",
        nullable: false,
        defaultValue: false
      );

      migrationBuilder.AddColumn<DateTime>(
        name: "EmailVerificationOtpNextResendValidAt",
        table: "Users",
        type: "datetime2",
        nullable: true
      );

      migrationBuilder.AddColumn<DateTime>(
        name: "PasswordResetOtpNextResendValidAt",
        table: "Users",
        type: "datetime2",
        nullable: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "AccountVerified", table: "Users");

      migrationBuilder.DropColumn(name: "EmailVerificationOtpNextResendValidAt", table: "Users");

      migrationBuilder.DropColumn(name: "PasswordResetOtpNextResendValidAt", table: "Users");
    }
  }
}
