using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_FixedOptions_FixedOptionId",
                table: "Values");

            migrationBuilder.DropIndex(
                name: "IX_Values_FixedOptionId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FixedOptionId",
                table: "Values");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FixedOptionId",
                table: "Values",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Values_FixedOptionId",
                table: "Values",
                column: "FixedOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_FixedOptions_FixedOptionId",
                table: "Values",
                column: "FixedOptionId",
                principalTable: "FixedOptions",
                principalColumn: "FixedOptionId");
        }
    }
}
