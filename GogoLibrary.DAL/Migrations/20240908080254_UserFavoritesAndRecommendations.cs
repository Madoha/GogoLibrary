using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GogoLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserFavoritesAndRecommendations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBookRecommendation",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookRecommendation", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_UserBookRecommendation_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookRecommendation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBookRecommendation_BookId",
                table: "UserBookRecommendation",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBookRecommendation");
        }
    }
}
