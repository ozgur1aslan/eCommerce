using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FixedOptionId",
                table: "Values",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FixedOptions",
                columns: table => new
                {
                    FixedOptionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedOptions", x => x.FixedOptionId);
                });

            migrationBuilder.CreateTable(
                name: "FixedValues",
                columns: table => new
                {
                    FixedValueId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ValueText = table.Column<string>(type: "TEXT", nullable: true),
                    OptionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedValues", x => x.FixedValueId);
                    table.ForeignKey(
                        name: "FK_FixedValues_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "OptionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_FixedOptionId",
                table: "Values",
                column: "FixedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedValues_OptionId",
                table: "FixedValues",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_FixedOptions_FixedOptionId",
                table: "Values",
                column: "FixedOptionId",
                principalTable: "FixedOptions",
                principalColumn: "FixedOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_FixedOptions_FixedOptionId",
                table: "Values");

            migrationBuilder.DropTable(
                name: "FixedOptions");

            migrationBuilder.DropTable(
                name: "FixedValues");

            migrationBuilder.DropIndex(
                name: "IX_Values_FixedOptionId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FixedOptionId",
                table: "Values");
        }
    }
}
