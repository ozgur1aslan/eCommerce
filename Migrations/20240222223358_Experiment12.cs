using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FixedValues");

            migrationBuilder.DropTable(
                name: "OptionProductVariant");

            migrationBuilder.DropTable(
                name: "FixedOptions");

            migrationBuilder.AddColumn<int>(
                name: "OptionId",
                table: "ProductVariants",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValueId",
                table: "ProductVariants",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_OptionId",
                table: "ProductVariants",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ValueId",
                table: "ProductVariants",
                column: "ValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Options_OptionId",
                table: "ProductVariants",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Values_ValueId",
                table: "ProductVariants",
                column: "ValueId",
                principalTable: "Values",
                principalColumn: "ValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Options_OptionId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Values_ValueId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_OptionId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ValueId",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "ValueId",
                table: "ProductVariants");

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
                name: "OptionProductVariant",
                columns: table => new
                {
                    OptionsOptionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductVariantsProductVariantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionProductVariant", x => new { x.OptionsOptionId, x.ProductVariantsProductVariantId });
                    table.ForeignKey(
                        name: "FK_OptionProductVariant_Options_OptionsOptionId",
                        column: x => x.OptionsOptionId,
                        principalTable: "Options",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OptionProductVariant_ProductVariants_ProductVariantsProductVariantId",
                        column: x => x.ProductVariantsProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "ProductVariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FixedValues",
                columns: table => new
                {
                    FixedValueId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FixedOptionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ValueText = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedValues", x => x.FixedValueId);
                    table.ForeignKey(
                        name: "FK_FixedValues_FixedOptions_FixedOptionId",
                        column: x => x.FixedOptionId,
                        principalTable: "FixedOptions",
                        principalColumn: "FixedOptionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixedValues_FixedOptionId",
                table: "FixedValues",
                column: "FixedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionProductVariant_ProductVariantsProductVariantId",
                table: "OptionProductVariant",
                column: "ProductVariantsProductVariantId");
        }
    }
}
