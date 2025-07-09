using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetMovilAPI.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryID", "ProductID" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryID", "ProductID" },
                keyValues: new object[] { 1, 1 });
        }
    }
}
