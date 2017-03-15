using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ResumeManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldOfStudies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOfStudies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    GitHub = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    LinkedIn = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: false),
                    PersonalSkills = table.Column<string>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    PhotoFileType = table.Column<string>(nullable: true),
                    References = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    DateCompleted = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    GitHub = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LinkedIn = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    PersonalSkills = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    PhotoFileType = table.Column<string>(nullable: true),
                    References = table.Column<string>(nullable: true),
                    ResumeId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeDrafts_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResumeDrafts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Degree = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FieldOfStudy = table.Column<string>(nullable: true),
                    FromYear = table.Column<int>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    ResumeId = table.Column<int>(nullable: false),
                    School = table.Column<string>(nullable: false),
                    ToYear = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<string>(nullable: false),
                    CurrentlyWorking = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    ResumeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageName = table.Column<string>(nullable: false),
                    ResumeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeLanguages_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResumeId = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeSkills_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DraftEducations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Degree = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FieldOfStudy = table.Column<string>(nullable: true),
                    FromYear = table.Column<int>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    ResumeDraftId = table.Column<int>(nullable: false),
                    School = table.Column<string>(nullable: true),
                    ToYear = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftEducations_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DraftExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<string>(nullable: true),
                    CurrentlyWorking = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    ResumeDraftId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftExperiences_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeDraftInterests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InterestName = table.Column<string>(nullable: false),
                    ResumeDraftId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeDraftInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeDraftInterests_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeDraftLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageName = table.Column<string>(nullable: false),
                    ResumeDraftId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeDraftLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeDraftLanguages_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeDraftSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResumeDraftId = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeDraftSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeDraftSkills_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeInterests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InterestName = table.Column<string>(nullable: false),
                    ResumeDraftId = table.Column<int>(nullable: true),
                    ResumeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeInterests_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResumeInterests_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserId",
                table: "Resumes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeDrafts_ResumeId",
                table: "ResumeDrafts",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeDrafts_UserId",
                table: "ResumeDrafts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftEducations_ResumeDraftId",
                table: "DraftEducations",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftExperiences_ResumeDraftId",
                table: "DraftExperiences",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeDraftInterests_ResumeDraftId",
                table: "ResumeDraftInterests",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeDraftLanguages_ResumeDraftId",
                table: "ResumeDraftLanguages",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeDraftLanguages_LanguageName_ResumeDraftId",
                table: "ResumeDraftLanguages",
                columns: new[] { "LanguageName", "ResumeDraftId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResumeDraftSkills_ResumeDraftId",
                table: "ResumeDraftSkills",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeDraftSkills_SkillName_ResumeDraftId",
                table: "ResumeDraftSkills",
                columns: new[] { "SkillName", "ResumeDraftId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_ResumeId",
                table: "Educations",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ResumeId",
                table: "Experiences",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeInterests_ResumeDraftId",
                table: "ResumeInterests",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeInterests_ResumeId",
                table: "ResumeInterests",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeLanguages_ResumeId",
                table: "ResumeLanguages",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeLanguages_LanguageName_ResumeId",
                table: "ResumeLanguages",
                columns: new[] { "LanguageName", "ResumeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_SkillName_ResumeId",
                table: "ResumeSkills",
                columns: new[] { "SkillName", "ResumeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldOfStudies");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "DraftEducations");

            migrationBuilder.DropTable(
                name: "DraftExperiences");

            migrationBuilder.DropTable(
                name: "ResumeDraftInterests");

            migrationBuilder.DropTable(
                name: "ResumeDraftLanguages");

            migrationBuilder.DropTable(
                name: "ResumeDraftSkills");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "ResumeInterests");

            migrationBuilder.DropTable(
                name: "ResumeLanguages");

            migrationBuilder.DropTable(
                name: "ResumeSkills");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "ResumeDrafts");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
