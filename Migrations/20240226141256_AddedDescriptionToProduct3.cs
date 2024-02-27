using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddedDescriptionToProduct3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Variants");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Variants",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Variants",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
