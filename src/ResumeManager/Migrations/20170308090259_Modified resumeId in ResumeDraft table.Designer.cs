using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.Migrations
{
    [DbContext(typeof(ResumeManagerDbContext))]
    [Migration("20170308090259_Modified resumeId in ResumeDraft table")]
    partial class ModifiedresumeIdinResumeDrafttable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseName")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("InstitutionName");

                    b.Property<string>("OtherInformation");

                    b.Property<int>("ResumeId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.DraftCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseName")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("InstitutionName");

                    b.Property<string>("OtherInformation");

                    b.Property<int>("ResumeDraftId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("DraftCourses");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.DraftJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("JobTitle")
                        .IsRequired();

                    b.Property<string>("OtherInformation");

                    b.Property<string>("Responsabilities")
                        .IsRequired();

                    b.Property<int>("ResumeDraftId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("DraftJobs");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.DraftQualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAquired");

                    b.Property<string>("InstitutionName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OtherInformation");

                    b.Property<int>("ResumeDraftId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("DraftQualifications");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("JobTitle")
                        .IsRequired();

                    b.Property<string>("OtherInformation");

                    b.Property<string>("Responsabilities")
                        .IsRequired();

                    b.Property<int>("ResumeId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Qualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAquired");

                    b.Property<string>("InstitutionName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OtherInformation");

                    b.Property<int>("ResumeId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Resume", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("GitHub");

                    b.Property<string>("Interests")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("LinkedIn");

                    b.Property<string>("Mobile")
                        .IsRequired();

                    b.Property<string>("PersonalSkills")
                        .IsRequired();

                    b.Property<byte[]>("Photo");

                    b.Property<string>("PhotoFileType");

                    b.Property<string>("References")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("DateCompleted");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("GitHub");

                    b.Property<string>("Interests");

                    b.Property<string>("LastName");

                    b.Property<string>("LinkedIn");

                    b.Property<string>("Mobile");

                    b.Property<string>("PersonalSkills");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("PhotoFileType");

                    b.Property<string>("References");

                    b.Property<int?>("ResumeId");

                    b.Property<string>("Status");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.HasIndex("UserId");

                    b.ToTable("ResumeDrafts");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName")
                        .IsRequired();

                    b.Property<int>("ResumeDraftId");

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("ResumeDraftLanguages");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ResumeDraftId");

                    b.Property<string>("SkillName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("ResumeDraftSkills");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeLanguage", b =>
                {
                    b.Property<int>("ResumeId");

                    b.Property<string>("LanguageName");

                    b.Property<int>("Id");

                    b.HasKey("ResumeId", "LanguageName");

                    b.ToTable("ResumeLanguages");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeSkill", b =>
                {
                    b.Property<int>("ResumeId");

                    b.Property<string>("SkillName");

                    b.Property<int>("Id");

                    b.HasKey("ResumeId", "SkillName");

                    b.ToTable("ResumeSkills");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Course", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("ResumeCourses")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.DraftCourse", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("DraftCourses")
                        .HasForeignKey("ResumeDraftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.DraftJob", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("DraftJobs")
                        .HasForeignKey("ResumeDraftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.DraftQualification", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("DraftQualifications")
                        .HasForeignKey("ResumeDraftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Job", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("ResumeJobs")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Qualification", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("ResumeQualifications")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Resume", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.User", "User")
                        .WithMany("Resumes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraft", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("ResumeDrafts")
                        .HasForeignKey("ResumeId");

                    b.HasOne("ResumeManager.DataAccess.Models.User", "User")
                        .WithMany("ResumeDrafts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftLanguage", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("ResumeDraftLanguages")
                        .HasForeignKey("ResumeDraftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftSkill", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("ResumeDraftSkills")
                        .HasForeignKey("ResumeDraftId")
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
                        .WithMany("ResumeSkills")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
