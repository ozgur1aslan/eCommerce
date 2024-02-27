using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddedComment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Variants_VariantId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_VariantId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "Comment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "Comment",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_VariantId",
                table: "Comment",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Variants_VariantId",
                table: "Comment",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "VariantId");
        }
    }
}
