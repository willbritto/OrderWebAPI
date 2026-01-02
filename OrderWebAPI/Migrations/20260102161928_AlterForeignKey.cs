using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderModels_categoryModels_CategoryModelCategoryId",
                table: "orderModels");

            migrationBuilder.DropIndex(
                name: "IX_orderModels_CategoryModelCategoryId",
                table: "orderModels");

            migrationBuilder.DropColumn(
                name: "CategoryModelCategoryId",
                table: "orderModels");

            migrationBuilder.CreateIndex(
                name: "IX_orderModels_CategoryId",
                table: "orderModels",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderModels_categoryModels_CategoryId",
                table: "orderModels",
                column: "CategoryId",
                principalTable: "categoryModels",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderModels_categoryModels_CategoryId",
                table: "orderModels");

            migrationBuilder.DropIndex(
                name: "IX_orderModels_CategoryId",
                table: "orderModels");

            migrationBuilder.AddColumn<int>(
                name: "CategoryModelCategoryId",
                table: "orderModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orderModels_CategoryModelCategoryId",
                table: "orderModels",
                column: "CategoryModelCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderModels_categoryModels_CategoryModelCategoryId",
                table: "orderModels",
                column: "CategoryModelCategoryId",
                principalTable: "categoryModels",
                principalColumn: "CategoryId");
        }
    }
}
