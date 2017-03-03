using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeManager.Migrations
{
    public partial class AddedPhotopropertytomodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoFileType",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "Resumes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Resumes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoFileType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Resumes");
        }
    }
}
