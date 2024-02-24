using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_ProductVariants_ProductVariantId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_ProductVariantId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "Options");

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

            migrationBuilder.CreateIndex(
                name: "IX_OptionProductVariant_ProductVariantsProductVariantId",
                table: "OptionProductVariant",
                column: "ProductVariantsProductVariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionProductVariant");

            migrationBuilder.AddColumn<int>(
                name: "ProductVariantId",
                table: "Options",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductVariantId",
                table: "Options",
                column: "ProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_ProductVariants_ProductVariantId",
                table: "Options",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId");
        }
    }
}
