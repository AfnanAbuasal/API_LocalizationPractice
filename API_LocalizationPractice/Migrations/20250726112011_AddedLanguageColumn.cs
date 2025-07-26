using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_LocalizationPractice.Migrations
{
    /// <inheritdoc />
    public partial class AddedLanguageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CategoryTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "CategoryTranslations");
        }
    }
}
