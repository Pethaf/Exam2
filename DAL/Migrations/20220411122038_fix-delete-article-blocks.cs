using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class fixdeletearticleblocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Block_Articles_ArticleId",
                table: "Block");

            migrationBuilder.AddForeignKey(
                name: "FK_Block_Articles_ArticleId",
                table: "Block",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Block_Articles_ArticleId",
                table: "Block");

            migrationBuilder.AddForeignKey(
                name: "FK_Block_Articles_ArticleId",
                table: "Block",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }
    }
}
