using Microsoft.EntityFrameworkCore;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeManagerDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ResumeDraft> ResumeDrafts { get; set; }
        public DbSet<ResumeSkill> ResumeSkills { get; set; }
        public DbSet<ResumeDraftSkill> ResumeDraftSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ResumeLanguage> ResumeLanguages { get; set; }
        public DbSet<ResumeDraftLanguage> ResumeDraftLanguages { get; set; }
        public DbSet<DraftCourse> DraftCourses { get; set; }
        public DbSet<DraftJob> DraftJobs { get; set; }
        public DbSet<DraftQualification> DraftQualifications { get; set; }

        public ResumeManagerDbContext(DbContextOptions<ResumeManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResumeSkill>()
                .HasKey(c => new { c.ResumeId, c.SkillName });

            modelBuilder.Entity<ResumeDraftSkill>()
                .HasKey(c => new { c.ResumeDraftId, c.SkillName });

            modelBuilder.Entity<ResumeLanguage>()
                .HasKey(c => new { c.ResumeId, c.LanguageName });           

            modelBuilder.Entity<User>()
               .HasMany(c => c.ResumeDrafts)
               .WithOne(m => m.User)
               .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
        }
    }
}