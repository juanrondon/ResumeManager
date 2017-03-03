using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeManager.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoFileType",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Resumes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoFileType",
                table: "Resumes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "PhotoFileType",
                table: "Resumes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoFileType",
                table: "Users",
                nullable: true);
        }
    }
}
