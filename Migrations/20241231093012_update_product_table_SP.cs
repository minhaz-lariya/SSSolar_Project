using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSSolar_Project.Migrations
{
    /// <inheritdoc />
    public partial class update_product_table_SP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isSpecialProduct",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSpecialProduct",
                table: "Products");
        }
    }
}
