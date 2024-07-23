using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relations.Migrations
{
    /// <inheritdoc />
    public partial class Indexex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name_Nickname",
                table: "Authors",
                columns: new[] { "Name", "Nickname" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authors_Name_Nickname",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Authors");
        }
    }
}
