using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pustokApp.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "H.G. Wells" },
                    { 2, "J.D. Kurtness" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Code", "Desc", "DiscountPercent", "HoverUrl", "InStock", "IsFeatured", "IsNew", "MainUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, 1001, "Cover Up Front Of Books And Leave Summary", 10, "~/image/products/product-1-2.jpg", true, true, true, "~/image/products/product-1-1.jpg", "De Vengeance", 78.090000000000003 },
                    { 2, 2, 1002, "Cover Up Front Of Books And Leave Summary", 5, "~/image/products/product-2-2.jpg", true, true, false, "~/image/products/product-2-1.jpg", "De Vengeance", 78.090000000000003 }
                });
        }
    }
}
