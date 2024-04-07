using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangPrac.Migrations
{
    /// <inheritdoc />
    public partial class LanguageTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageType",
                table: "UserLanguages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LanguageType",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageType",
                table: "UserLanguages");

            migrationBuilder.DropColumn(
                name: "LanguageType",
                table: "Languages");
        }
    }
}
