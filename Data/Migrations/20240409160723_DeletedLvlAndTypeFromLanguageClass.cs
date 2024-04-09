using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangPrac.Migrations
{
    /// <inheritdoc />
    public partial class DeletedLvlAndTypeFromLanguageClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageLvl",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "LanguageType",
                table: "Languages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageLvl",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageType",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
