using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangPrac.Migrations
{
    /// <inheritdoc />
    public partial class HasPartnerInitiatedEndAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPartnerInitiatedEnd",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPartnerInitiatedEnd",
                table: "Notifications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
