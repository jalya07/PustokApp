using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pustokApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookImagePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MainUrl", "HoverUrl" },
                values: new object[] { "product-1-1.jpg", "product-1-2.jpg" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MainUrl", "HoverUrl" },
                values: new object[] { "product-2-1.jpg", "product-2-2.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MainUrl", "HoverUrl" },
                values: new object[] { "~/image/products/product-1-1.jpg", "~/image/products/product-1-2.jpg" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MainUrl", "HoverUrl" },
                values: new object[] { "~/image/products/product-2-1.jpg", "~/image/products/product-2-2.jpg" });
        }
    }
}
