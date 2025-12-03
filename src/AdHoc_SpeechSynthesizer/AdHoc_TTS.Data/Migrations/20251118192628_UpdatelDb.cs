using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdHoc_SpeechSynthesizer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatelDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SsmlContent",
                schema: "dbo",
                table: "MessageTemplate",
                type: "nvarchar(max)",
                maxLength: 150000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SsmlContent",
                schema: "dbo",
                table: "MessageTemplate",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 150000);
        }
    }
}
