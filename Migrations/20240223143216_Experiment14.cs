using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_ProductVariants_ProductVariantId",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "ProductVariantValue");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "Pictures",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_ProductVariantId",
                table: "Pictures",
                newName: "IX_Pictures_VariantId");

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ValueVariant",
                columns: table => new
                {
                    ValuesValueId = table.Column<int>(type: "INTEGER", nullable: false),
                    VariantsVariantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueVariant", x => new { x.ValuesValueId, x.VariantsVariantId });
                    table.ForeignKey(
                        name: "FK_ValueVariant_Values_ValuesValueId",
                        column: x => x.ValuesValueId,
                        principalTable: "Values",
                        principalColumn: "ValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValueVariant_Variants_VariantsVariantId",
                        column: x => x.VariantsVariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValueVariant_VariantsVariantId",
                table: "ValueVariant",
                column: "VariantsVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Variants_VariantId",
                table: "Pictures",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Variants_VariantId",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "ValueVariant");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "Pictures",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_VariantId",
                table: "Pictures",
                newName: "IX_Pictures_ProductVariantId");

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    ProductVariantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    DiscountedPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.ProductVariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

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
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantValue_ValuesValueId",
                table: "ProductVariantValue",
                column: "ValuesValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_ProductVariants_ProductVariantId",
                table: "Pictures",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
