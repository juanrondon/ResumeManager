using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeManager.Migrations
{
    public partial class ModifiedresumeIdinResumeDrafttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeDrafts_Resumes_ResumeId",
                table: "ResumeDrafts");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "ResumeDrafts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeDrafts_Resumes_ResumeId",
                table: "ResumeDrafts",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeDrafts_Resumes_ResumeId",
                table: "ResumeDrafts");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "ResumeDrafts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeDrafts_Resumes_ResumeId",
                table: "ResumeDrafts",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
