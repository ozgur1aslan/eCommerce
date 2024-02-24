using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Product2Id",
                table: "Tags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Product2s",
                columns: table => new
                {
                    Product2Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<bool>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    SeasonId = table.Column<int>(type: "INTEGER", nullable: true),
                    BrandId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product2s", x => x.Product2Id);
                    table.ForeignKey(
                        name: "FK_Product2s_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId");
                    table.ForeignKey(
                        name: "FK_Product2s_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Product2s_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId");
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    ProductVariantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Product2Id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.ProductVariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Product2s_Product2Id",
                        column: x => x.Product2Id,
                        principalTable: "Product2s",
                        principalColumn: "Product2Id");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionName = table.Column<string>(type: "TEXT", nullable: true),
                    OptionId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductVariantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Options_Options_OptionId1",
                        column: x => x.OptionId1,
                        principalTable: "Options",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_Options_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "ProductVariantId");
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    ValueId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ValueText = table.Column<string>(type: "TEXT", nullable: true),
                    OptionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.ValueId);
                    table.ForeignKey(
                        name: "FK_Values_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "OptionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Product2Id",
                table: "Tags",
                column: "Product2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionId1",
                table: "Options",
                column: "OptionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductVariantId",
                table: "Options",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Product2s_BrandId",
                table: "Product2s",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product2s_CategoryId",
                table: "Product2s",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product2s_SeasonId",
                table: "Product2s",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_Product2Id",
                table: "ProductVariants",
                column: "Product2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Values_OptionId",
                table: "Values",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Product2s_Product2Id",
                table: "Tags",
                column: "Product2Id",
                principalTable: "Product2s",
                principalColumn: "Product2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Product2s_Product2Id",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Product2s");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Product2Id",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Product2Id",
                table: "Tags");
        }
    }
}
