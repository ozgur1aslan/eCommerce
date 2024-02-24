using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedValues_Options_OptionId",
                table: "FixedValues");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "FixedValues",
                newName: "FixedOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_FixedValues_OptionId",
                table: "FixedValues",
                newName: "IX_FixedValues_FixedOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedValues_FixedOptions_FixedOptionId",
                table: "FixedValues",
                column: "FixedOptionId",
                principalTable: "FixedOptions",
                principalColumn: "FixedOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedValues_FixedOptions_FixedOptionId",
                table: "FixedValues");

            migrationBuilder.RenameColumn(
                name: "FixedOptionId",
                table: "FixedValues",
                newName: "OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_FixedValues_FixedOptionId",
                table: "FixedValues",
                newName: "IX_FixedValues_OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedValues_Options_OptionId",
                table: "FixedValues",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "OptionId");
        }
    }
}
