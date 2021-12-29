using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectDAW.Migrations
{
    public partial class ModifiedBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookContent",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookContent", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookContent_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookContent");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Book");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Book",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
