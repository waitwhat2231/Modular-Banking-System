using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InterestColumnsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccuredInterest",
                schema: "Accounts",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDailyInterestCalculation",
                schema: "Accounts",
                table: "Accounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMonthlyInterestDeposit",
                schema: "Accounts",
                table: "Accounts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccuredInterest",
                schema: "Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastDailyInterestCalculation",
                schema: "Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastMonthlyInterestDeposit",
                schema: "Accounts",
                table: "Accounts");
        }
    }
}
