using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSSolar_Project.Migrations
{
    /// <inheritdoc />
    public partial class update_product_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BriefDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SEOKeywords",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BriefDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SEOKeywords",
                table: "Products");
        }
    }
}
