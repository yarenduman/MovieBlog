using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieBlog.Data.Migrations
{
    public partial class identityExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "MyList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "MyBlog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyList_AuthorId",
                table: "MyList",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MyBlog_AuthorId",
                table: "MyBlog",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyBlog_AspNetUsers_AuthorId",
                table: "MyBlog",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyList_AspNetUsers_AuthorId",
                table: "MyList",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyBlog_AspNetUsers_AuthorId",
                table: "MyBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_MyList_AspNetUsers_AuthorId",
                table: "MyList");

            migrationBuilder.DropIndex(
                name: "IX_MyList_AuthorId",
                table: "MyList");

            migrationBuilder.DropIndex(
                name: "IX_MyBlog_AuthorId",
                table: "MyBlog");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "MyList");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "MyBlog");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
