using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DictionaryApi.Migrations
{
    /// <inheritdoc />
    public partial class use_plan_english_georgian_name_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameTranslations",
                schema: "Dictionary",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "NameTranslations",
                schema: "Dictionary",
                table: "SubTopic");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                schema: "Dictionary",
                table: "Topic",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeorgianName",
                schema: "Dictionary",
                table: "Topic",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                schema: "Dictionary",
                table: "SubTopic",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeorgianName",
                schema: "Dictionary",
                table: "SubTopic",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Entry",
                schema: "Dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FunctionalLabel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StylisticQualification = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Source = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Idiom = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Synonym = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UsageNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TopicId = table.Column<int>(type: "integer", nullable: false),
                    SubTopicId = table.Column<int>(type: "integer", nullable: false),
                    DefinitionTranslations = table.Column<string>(type: "jsonb", nullable: true),
                    HeadwordTranslations = table.Column<string>(type: "jsonb", nullable: true),
                    IllustrationSentenceTranslations = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_SubTopic_SubTopicId",
                        column: x => x.SubTopicId,
                        principalSchema: "Dictionary",
                        principalTable: "SubTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entry_Topic_TopicId",
                        column: x => x.TopicId,
                        principalSchema: "Dictionary",
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entry_SubTopicId",
                schema: "Dictionary",
                table: "Entry",
                column: "SubTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_TopicId",
                schema: "Dictionary",
                table: "Entry",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entry",
                schema: "Dictionary");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                schema: "Dictionary",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "GeorgianName",
                schema: "Dictionary",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                schema: "Dictionary",
                table: "SubTopic");

            migrationBuilder.DropColumn(
                name: "GeorgianName",
                schema: "Dictionary",
                table: "SubTopic");

            migrationBuilder.AddColumn<string>(
                name: "NameTranslations",
                schema: "Dictionary",
                table: "Topic",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameTranslations",
                schema: "Dictionary",
                table: "SubTopic",
                type: "jsonb",
                nullable: true);
        }
    }
}
