using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSSolar_Project.Migrations
{
    /// <inheritdoc />
    public partial class update_productsMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isShowPrice",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isShowPrice",
                table: "Products");
        }
    }
}
