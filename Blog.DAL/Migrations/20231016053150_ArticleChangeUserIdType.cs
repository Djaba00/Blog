using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ArticleChangeUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_UserProfiles_UserProfileId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserProfileId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_UserProfiles_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_UserProfiles_UserId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Articles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserProfileId",
                table: "Articles",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_UserProfiles_UserProfileId",
                table: "Articles",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
