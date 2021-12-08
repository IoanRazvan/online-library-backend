using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectDAW.Migrations
{
    public partial class AddLibraryBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Library_Users_OwnerId",
                table: "Library");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Library",
                table: "Library");

            migrationBuilder.RenameTable(
                name: "Library",
                newName: "Libraries");

            migrationBuilder.RenameIndex(
                name: "IX_Library_OwnerId",
                table: "Libraries",
                newName: "IX_Libraries_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LibraryBooks",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryBooks", x => new { x.BookId, x.LibraryId });
                    table.ForeignKey(
                        name: "FK_LibraryBooks_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryBooks_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryBooks_LibraryId",
                table: "LibraryBooks",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Users_OwnerId",
                table: "Libraries",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Users_OwnerId",
                table: "Libraries");

            migrationBuilder.DropTable(
                name: "LibraryBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libraries",
                table: "Libraries");

            migrationBuilder.RenameTable(
                name: "Libraries",
                newName: "Library");

            migrationBuilder.RenameIndex(
                name: "IX_Libraries_OwnerId",
                table: "Library",
                newName: "IX_Library_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Library",
                table: "Library",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Library_Users_OwnerId",
                table: "Library",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
