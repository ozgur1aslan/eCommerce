using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class Experiment13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Product2s_Product2Id",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Product2s_Product2Id",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Product2s");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Product2Id",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Product2Id",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Product2Id",
                table: "ProductVariants",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_Product2Id",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Pictures",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductVariants",
                newName: "Product2Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_Product2Id");

            migrationBuilder.AddColumn<int>(
                name: "Product2Id",
                table: "Tags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountedPrice",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Pictures",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Product2s",
                columns: table => new
                {
                    Product2Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BrandId = table.Column<int>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    GenderId = table.Column<int>(type: "INTEGER", nullable: true),
                    SeasonId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<bool>(type: "INTEGER", nullable: true)
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
                        name: "FK_Product2s_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId");
                    table.ForeignKey(
                        name: "FK_Product2s_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Product2Id",
                table: "Tags",
                column: "Product2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product2s_BrandId",
                table: "Product2s",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product2s_CategoryId",
                table: "Product2s",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product2s_GenderId",
                table: "Product2s",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product2s_SeasonId",
                table: "Product2s",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Product2s_Product2Id",
                table: "ProductVariants",
                column: "Product2Id",
                principalTable: "Product2s",
                principalColumn: "Product2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Product2s_Product2Id",
                table: "Tags",
                column: "Product2Id",
                principalTable: "Product2s",
                principalColumn: "Product2Id");
        }
    }
}
