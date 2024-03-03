using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Variants_VariantId",
                table: "PurchasedItems");

            migrationBuilder.AlterColumn<int>(
                name: "VariantId",
                table: "PurchasedItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Variants_VariantId",
                table: "PurchasedItems",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "VariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Variants_VariantId",
                table: "PurchasedItems");

            migrationBuilder.AlterColumn<int>(
                name: "VariantId",
                table: "PurchasedItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Variants_VariantId",
                table: "PurchasedItems",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
