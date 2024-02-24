using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class RelationAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandSeason");

            migrationBuilder.DropTable(
                name: "CategorySeason");

            migrationBuilder.CreateTable(
                name: "BrandProduct",
                columns: table => new
                {
                    BrandsBrandId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandProduct", x => new { x.BrandsBrandId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_BrandProduct_Brands_BrandsBrandId",
                        column: x => x.BrandsBrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSeason",
                columns: table => new
                {
                    ProductsProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonsSeasonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSeason", x => new { x.ProductsProductId, x.SeasonsSeasonId });
                    table.ForeignKey(
                        name: "FK_ProductSeason_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSeason_Seasons_SeasonsSeasonId",
                        column: x => x.SeasonsSeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandProduct_ProductsProductId",
                table: "BrandProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSeason_SeasonsSeasonId",
                table: "ProductSeason",
                column: "SeasonsSeasonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandProduct");

            migrationBuilder.DropTable(
                name: "ProductSeason");

            migrationBuilder.CreateTable(
                name: "BrandSeason",
                columns: table => new
                {
                    BrandsBrandId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonsSeasonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandSeason", x => new { x.BrandsBrandId, x.SeasonsSeasonId });
                    table.ForeignKey(
                        name: "FK_BrandSeason_Brands_BrandsBrandId",
                        column: x => x.BrandsBrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandSeason_Seasons_SeasonsSeasonId",
                        column: x => x.SeasonsSeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorySeason",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonsSeasonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySeason", x => new { x.CategoriesCategoryId, x.SeasonsSeasonId });
                    table.ForeignKey(
                        name: "FK_CategorySeason_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySeason_Seasons_SeasonsSeasonId",
                        column: x => x.SeasonsSeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandSeason_SeasonsSeasonId",
                table: "BrandSeason",
                column: "SeasonsSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySeason_SeasonsSeasonId",
                table: "CategorySeason",
                column: "SeasonsSeasonId");
        }
    }
}
