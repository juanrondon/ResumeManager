using Microsoft.EntityFrameworkCore;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeManagerDbContext : DbContext
    {
        public DbSet<ResumeExperience> Experiences { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ResumeEducation> Educations { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ResumeDraft> ResumeDrafts { get; set; }
        public DbSet<ResumeSkill> ResumeSkills { get; set; }
        public DbSet<ResumeDraftSkill> ResumeDraftSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<FieldOfStudy> FieldOfStudies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ResumeLanguage> ResumeLanguages { get; set; }
        public DbSet<ResumeDraftLanguage> ResumeDraftLanguages { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<ResumeDraftInterest> ResumeDraftInterests { get; set; }
        public DbSet<ResumeInterest> ResumeInterests { get; set; }
        public DbSet<ResumeDraftExperience> DraftExperiences { get; set; }
        public DbSet<ResumeDraftEducation> DraftEducations { get; set; }

        public ResumeManagerDbContext(DbContextOptions<ResumeManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResumeDraftLanguage>().HasIndex(p => new { p.LanguageName, p.ResumeDraftId }).IsUnique();
            modelBuilder.Entity<ResumeLanguage>().HasIndex(p => new { p.LanguageName, p.ResumeId }).IsUnique();
            modelBuilder.Entity<ResumeDraftSkill>().HasIndex(p => new { p.SkillName, p.ResumeDraftId }).IsUnique();
            modelBuilder.Entity<ResumeSkill>().HasIndex(p => new { p.SkillName, p.ResumeId }).IsUnique();

            modelBuilder.Entity<User>()
               .HasMany(c => c.ResumeDrafts)
               .WithOne(m => m.User)
               .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
        }
    }
}