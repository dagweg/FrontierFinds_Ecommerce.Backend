using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class resendotpfailstreak : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<int>(
        name: "EmailVerificationOtp_ResendFailStreak",
        table: "Users",
        type: "int",
        nullable: true
      );

      migrationBuilder.AddColumn<int>(
        name: "PasswordResetOtp_ResendFailStreak",
        table: "Users",
        type: "int",
        nullable: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "EmailVerificationOtp_ResendFailStreak", table: "Users");

      migrationBuilder.DropColumn(name: "PasswordResetOtp_ResendFailStreak", table: "Users");
    }
  }
}
