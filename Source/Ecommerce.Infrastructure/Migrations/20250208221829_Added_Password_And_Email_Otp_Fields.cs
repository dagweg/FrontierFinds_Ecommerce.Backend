using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Password_And_Email_Otp_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
              name: "EmailVerificationOtp_Expiry",
              table: "Users",
              type: "datetime2",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "EmailVerificationOtp_Value",
              table: "Users",
              type: "nvarchar(max)",
              nullable: true
            );

            migrationBuilder.AddColumn<DateTime>(
              name: "PasswordResetOtp_Expiry",
              table: "Users",
              type: "datetime2",
              nullable: true
            );

            migrationBuilder.AddColumn<string>(
              name: "PasswordResetOtp_Value",
              table: "Users",
              type: "nvarchar(max)",
              nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "EmailVerificationOtp_Expiry", table: "Users");

            migrationBuilder.DropColumn(name: "EmailVerificationOtp_Value", table: "Users");

            migrationBuilder.DropColumn(name: "PasswordResetOtp_Expiry", table: "Users");

            migrationBuilder.DropColumn(name: "PasswordResetOtp_Value", table: "Users");
        }
    }
}
