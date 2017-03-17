using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.Migrations
{
    [DbContext(typeof(ResumeManagerDbContext))]
    partial class ResumeManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ResumeManager.DataAccess.Models.FieldOfStudy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FieldOfStudies");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Interest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Interests");
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

            modelBuilder.Entity("ResumeManager.DataAccess.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Locations");
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

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftEducation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Degree");

                    b.Property<string>("Description");

                    b.Property<string>("FieldOfStudy");

                    b.Property<int?>("FromYear");

                    b.Property<string>("Grade");

                    b.Property<int>("ResumeDraftId");

                    b.Property<string>("School");

                    b.Property<int?>("ToYear");

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("DraftEducations");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company");

                    b.Property<bool>("CurrentlyWorking");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Location");

                    b.Property<int>("ResumeDraftId");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("DraftExperiences");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InterestName")
                        .IsRequired();

                    b.Property<int>("ResumeDraftId");

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.ToTable("ResumeDraftInterests");
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

                    b.HasIndex("LanguageName", "ResumeDraftId")
                        .IsUnique();

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

                    b.HasIndex("SkillName", "ResumeDraftId")
                        .IsUnique();

                    b.ToTable("ResumeDraftSkills");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeEducation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Degree")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("FieldOfStudy");

                    b.Property<int?>("FromYear");

                    b.Property<string>("Grade");

                    b.Property<int>("ResumeId");

                    b.Property<string>("School")
                        .IsRequired();

                    b.Property<int?>("ToYear");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company")
                        .IsRequired();

                    b.Property<bool>("CurrentlyWorking");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Location");

                    b.Property<int>("ResumeId");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InterestName")
                        .IsRequired();

                    b.Property<int?>("ResumeDraftId");

                    b.Property<int>("ResumeId");

                    b.HasKey("Id");

                    b.HasIndex("ResumeDraftId");

                    b.HasIndex("ResumeId");

                    b.ToTable("ResumeInterests");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName")
                        .IsRequired();

                    b.Property<int>("ResumeId");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.HasIndex("LanguageName", "ResumeId")
                        .IsUnique();

                    b.ToTable("ResumeLanguages");
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ResumeId");

                    b.Property<string>("SkillName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.HasIndex("SkillName", "ResumeId")
                        .IsUnique();

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

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftEducation", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("ResumeDraftEducations")
                        .HasForeignKey("ResumeDraftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftExperience", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("ResumeDraftExperiences")
                        .HasForeignKey("ResumeDraftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeDraftInterest", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "ResumeDraft")
                        .WithMany("ResumeDraftInterests")
                        .HasForeignKey("ResumeDraftId")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeEducation", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("ResumeEducations")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeExperience", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.Resume", "Resume")
                        .WithMany("ResumeExperiences")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ResumeManager.DataAccess.Models.ResumeInterest", b =>
                {
                    b.HasOne("ResumeManager.DataAccess.Models.ResumeDraft", "Resume")
                        .WithMany()
                        .HasForeignKey("ResumeDraftId");

                    b.HasOne("ResumeManager.DataAccess.Models.Resume")
                        .WithMany("ResumeInterests")
                        .HasForeignKey("ResumeId")
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
