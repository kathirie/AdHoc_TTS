
#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdHoc_SpeechSynthesizer.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "TtsVoice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "TtsVoice",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
