using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectDAW.Migrations
{
    public partial class ReversedRelationShipUserUserSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersSettings_UserSettingsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserSettingsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserSettingsId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UsersSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UsersSettings_UserId",
                table: "UsersSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSettings_Users_UserId",
                table: "UsersSettings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersSettings_Users_UserId",
                table: "UsersSettings");

            migrationBuilder.DropIndex(
                name: "IX_UsersSettings_UserId",
                table: "UsersSettings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersSettings");

            migrationBuilder.AddColumn<Guid>(
                name: "UserSettingsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserSettingsId",
                table: "Users",
                column: "UserSettingsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersSettings_UserSettingsId",
                table: "Users",
                column: "UserSettingsId",
                principalTable: "UsersSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
