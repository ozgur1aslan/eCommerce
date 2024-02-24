using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class ex1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ProductVariantValue",
                columns: table => new
                {
                    ProductVariantsProductVariantId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValuesValueId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantValue", x => new { x.ProductVariantsProductVariantId, x.ValuesValueId });
                    table.ForeignKey(
                        name: "FK_ProductVariantValue_ProductVariants_ProductVariantsProductVariantId",
                        column: x => x.ProductVariantsProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "ProductVariantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariantValue_Values_ValuesValueId",
                        column: x => x.ValuesValueId,
                        principalTable: "Values",
                        principalColumn: "ValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantValue_ValuesValueId",
                table: "ProductVariantValue",
                column: "ValuesValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVariantValue");

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
    }
}
