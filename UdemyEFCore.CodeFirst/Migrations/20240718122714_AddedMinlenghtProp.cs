using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyEFCore.CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class AddedMinlenghtProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProducts",
                table: "MyProducts");

            migrationBuilder.RenameTable(
                name: "MyProducts",
                newName: "Product Table");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product Table",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product Table",
                table: "Product Table",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Product Table",
                table: "Product Table");

            migrationBuilder.RenameTable(
                name: "Product Table",
                newName: "MyProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MyProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProducts",
                table: "MyProducts",
                column: "Id");
        }
    }
}
