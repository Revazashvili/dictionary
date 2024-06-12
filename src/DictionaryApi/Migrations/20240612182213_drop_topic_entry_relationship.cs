using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryApi.Migrations
{
    /// <inheritdoc />
    public partial class drop_topic_entry_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Topic_TopicId",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropIndex(
                name: "IX_Entry_TopicId",
                schema: "Dictionary",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "TopicId",
                schema: "Dictionary",
                table: "Entry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                schema: "Dictionary",
                table: "Entry",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Entry_TopicId",
                schema: "Dictionary",
                table: "Entry",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Topic_TopicId",
                schema: "Dictionary",
                table: "Entry",
                column: "TopicId",
                principalSchema: "Dictionary",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
