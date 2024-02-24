using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Options_OptionId1",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_OptionId1",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "OptionId1",
                table: "Options");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionId1",
                table: "Options",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionId1",
                table: "Options",
                column: "OptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Options_OptionId1",
                table: "Options",
                column: "OptionId1",
                principalTable: "Options",
                principalColumn: "OptionId");
        }
    }
}
