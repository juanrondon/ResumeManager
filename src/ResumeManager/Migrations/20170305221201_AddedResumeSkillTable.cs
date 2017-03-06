using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ResumeManager.Migrations
{
    public partial class AddedResumeSkillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ResumeId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "ResumeSkills",
                columns: table => new
                {
                    ResumeSkillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResumeId = table.Column<int>(nullable: false),
                    skillName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkills", x => x.ResumeSkillId);
                    table.ForeignKey(
                        name: "FK_ResumeSkills_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "ResumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResumeSkills");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Skills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ResumeId",
                table: "Skills",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "ResumeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
