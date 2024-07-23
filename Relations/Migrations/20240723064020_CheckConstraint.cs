using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relations.Migrations
{
    /// <inheritdoc />
    public partial class CheckConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authors_Name",
                table: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DiscountPrice",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name")
                .Annotation("SqlServer:Include", new[] { "Price", "Barcode" });

            migrationBuilder.AddCheckConstraint(
                name: "DiscountCheck",
                table: "Products",
                sql: "[Price]>[DiscountPrice]");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name",
                table: "Authors",
                column: "Name")
                .Annotation("SqlServer:Include", new[] { "Nickname" });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Nickname",
                table: "Authors",
                column: "Nickname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "DiscountCheck",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Authors_Name",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_Nickname",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name",
                table: "Authors",
                column: "Name");
        }
    }
}
