using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApi.Migrations
{
    /// <inheritdoc />
    public partial class use_plan_english_georgian_properties_in_entry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefinitionTranslations",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "HeadwordTranslations",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "IllustrationSentenceTranslations",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.AlterColumn<string>(
                name: "UsageNote",
                schema: "Dictionary",
                table: "Entry",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "EnglishDefinition",
                schema: "Dictionary",
                table: "Entry",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishHeadword",
                schema: "Dictionary",
                table: "Entry",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishIllustrationSentence",
                schema: "Dictionary",
                table: "Entry",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeorgianDefinition",
                schema: "Dictionary",
                table: "Entry",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeorgianHeadword",
                schema: "Dictionary",
                table: "Entry",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeorgianIllustrationSentence",
                schema: "Dictionary",
                table: "Entry",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishDefinition",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "EnglishHeadword",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "EnglishIllustrationSentence",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "GeorgianDefinition",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "GeorgianHeadword",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "GeorgianIllustrationSentence",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.AlterColumn<string>(
                name: "UsageNote",
                schema: "Dictionary",
                table: "Entry",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "DefinitionTranslations",
                schema: "Dictionary",
                table: "Entry",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadwordTranslations",
                schema: "Dictionary",
                table: "Entry",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IllustrationSentenceTranslations",
                schema: "Dictionary",
                table: "Entry",
                type: "jsonb",
                nullable: true);
        }
    }
}
