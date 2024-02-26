using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class UserModelChanges5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Products_ProductId",
                table: "WishlistItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "WishlistItems",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                newName: "IX_WishlistItems_VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Variants_VariantId",
                table: "WishlistItems",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Variants_VariantId",
                table: "WishlistItems");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "WishlistItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistItems_VariantId",
                table: "WishlistItems",
                newName: "IX_WishlistItems_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Products_ProductId",
                table: "WishlistItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
