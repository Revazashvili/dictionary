using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApi.Migrations
{
    /// <inheritdoc />
    public partial class sub_topic_entry_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_SubTopic_SubTopicId",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_SubTopic_SubTopicId",
                schema: "Dictionary",
                table: "Entry",
                column: "SubTopicId",
                principalSchema: "Dictionary",
                principalTable: "SubTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_SubTopic_SubTopicId",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_SubTopic_SubTopicId",
                schema: "Dictionary",
                table: "Entry",
                column: "SubTopicId",
                principalSchema: "Dictionary",
                principalTable: "SubTopic",
                principalColumn: "Id");
        }
    }
}
