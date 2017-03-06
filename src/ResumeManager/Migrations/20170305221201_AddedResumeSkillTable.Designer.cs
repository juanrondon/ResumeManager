using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.Migrations
{
    [DbContext(typeof(ResumeManagerDbContext))]
    [Migration("20170305221201_AddedResumeSkillTable")]
    partial class AddedResumeSkillTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseName")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("InstitutionName")
                        .IsRequired();

                    b.Property<string>("OtherInformation");

                    b.Property<int?>("ResumeId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("CourseId");

                    b.HasIndex("ResumeId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("JobResponsabilities")
                        .IsRequired();

                    b.Property<string>("JobTitle")
                        .IsRequired();

                    b.Property<string>("OtherInformation");

                    b.Property<int?>("ResumeId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("JobId");

                    b.HasIndex("ResumeId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Qualification", b =>
                {
                    b.Property<int>("QualificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAquired");

                    b.Property<string>("InstitutionName")
                        .IsRequired();

                    b.Property<string>("OtherInformation");

                    b.Property<string>("QualificationName")
                        .IsRequired();

                    b.Property<string>("QualificationType")
                        .IsRequired();

                    b.Property<int?>("ResumeId");

                    b.HasKey("QualificationId");

                    b.HasIndex("ResumeId");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Resume", b =>
                {
                    b.Property<int>("ResumeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("GitHub");

                    b.Property<string>("Interests");

                    b.Property<string>("LastName");

                    b.Property<string>("LinkedIn");

                    b.Property<string>("Mobile");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("PhotoFileType");

                    b.Property<string>("References");

                    b.Property<string>("Summary");

                    b.Property<int>("UserId");

                    b.HasKey("ResumeId");

                    b.HasIndex("UserId");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeLanguage", b =>
                {
                    b.Property<int>("ResumeLanguageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName")
                        .IsRequired();

                    b.Property<int>("ResumeId");

                    b.HasKey("ResumeLanguageId");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeLanguages");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeSkill", b =>
                {
                    b.Property<int>("ResumeSkillId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ResumeId");

                    b.Property<string>("skillName")
                        .IsRequired();

                    b.HasKey("ResumeSkillId");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeSkills");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("SkillId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Course", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume")
                        .WithMany("Courses")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Job", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume")
                        .WithMany("Jobs")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Qualification", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume")
                        .WithMany("Qualifications")
                        .HasForeignKey("ResumeId");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Resume", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeLanguage", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("ResumeLanguages")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeSkill", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("Skills")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
