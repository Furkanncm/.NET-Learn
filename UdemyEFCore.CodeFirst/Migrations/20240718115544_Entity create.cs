using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyEFCore.CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class Entitycreate : Migration
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProducts",
                table: "MyProducts",
                column: "Id");
        }
    }
}
