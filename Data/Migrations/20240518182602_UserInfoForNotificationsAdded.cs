using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangPrac.Migrations
{
    /// <inheritdoc />
    public partial class UserInfoForNotificationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserInfo",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserLanguagesInfo",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserInfo",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserLanguagesInfo",
                table: "Notifications");
        }
    }
}
