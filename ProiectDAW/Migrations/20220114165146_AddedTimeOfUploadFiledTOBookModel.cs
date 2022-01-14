﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectDAW.Migrations
{
    public partial class AddedTimeOfUploadFiledTOBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOfUpload",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOfUpload",
                table: "Books");
        }
    }
}
