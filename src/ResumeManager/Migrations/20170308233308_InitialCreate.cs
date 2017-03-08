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
                    Interests = table.Column<string>(nullable: false),
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
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseName = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    InstitutionName = table.Column<string>(nullable: true),
                    OtherInformation = table.Column<string>(nullable: true),
                    ResumeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    JobTitle = table.Column<string>(nullable: false),
                    OtherInformation = table.Column<string>(nullable: true),
                    Responsabilities = table.Column<string>(nullable: false),
                    ResumeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateAquired = table.Column<DateTime>(nullable: false),
                    InstitutionName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OtherInformation = table.Column<string>(nullable: true),
                    ResumeId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualifications_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
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
                    Interests = table.Column<string>(nullable: true),
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
                name: "ResumeLanguages",
                columns: table => new
                {
                    ResumeId = table.Column<int>(nullable: false),
                    LanguageName = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeLanguages", x => new { x.ResumeId, x.LanguageName });
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
                    ResumeId = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkills", x => new { x.ResumeId, x.SkillName });
                    table.ForeignKey(
                        name: "FK_ResumeSkills_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DraftCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseName = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    InstitutionName = table.Column<string>(nullable: true),
                    OtherInformation = table.Column<string>(nullable: true),
                    ResumeDraftId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftCourses_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DraftJobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    JobTitle = table.Column<string>(nullable: false),
                    OtherInformation = table.Column<string>(nullable: true),
                    Responsabilities = table.Column<string>(nullable: false),
                    ResumeDraftId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftJobs_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DraftQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateAquired = table.Column<DateTime>(nullable: false),
                    InstitutionName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OtherInformation = table.Column<string>(nullable: true),
                    ResumeDraftId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftQualifications_ResumeDrafts_ResumeDraftId",
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
                    ResumeDraftId = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeDraftSkills", x => new { x.ResumeDraftId, x.SkillName });
                    table.ForeignKey(
                        name: "FK_ResumeDraftSkills_ResumeDrafts_ResumeDraftId",
                        column: x => x.ResumeDraftId,
                        principalTable: "ResumeDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ResumeId",
                table: "Courses",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftCourses_ResumeDraftId",
                table: "DraftCourses",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftJobs_ResumeDraftId",
                table: "DraftJobs",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftQualifications_ResumeDraftId",
                table: "DraftQualifications",
                column: "ResumeDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ResumeId",
                table: "Jobs",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_ResumeId",
                table: "Qualifications",
                column: "ResumeId");

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
                name: "IX_ResumeDraftLanguages_ResumeDraftId",
                table: "ResumeDraftLanguages",
                column: "ResumeDraftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "DraftCourses");

            migrationBuilder.DropTable(
                name: "DraftJobs");

            migrationBuilder.DropTable(
                name: "DraftQualifications");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "ResumeDraftLanguages");

            migrationBuilder.DropTable(
                name: "ResumeDraftSkills");

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
