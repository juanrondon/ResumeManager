using System.Linq;
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
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ResumeLanguage> ResumeLanguages { get; set; }

        public ResumeManagerDbContext(DbContextOptions<ResumeManagerDbContext> options) : base(options) { }

        //Seedsomeinitialdata
        public void Seed()
        {
            string[] languages =
            {
                "Afrikaans",
                "Albanian",
                "Arabic",
                "Armenian",
                "Basque",
                "Bengali",
                "Bulgarian",
                "Catalan",
                "Cambodian",
                "Chinese(Mandarin)",
                "Croatian",
                "Czech",
                "Danish",
                "Dutch",
                "English",
                "Estonian",
                "Fiji",
                "Finnish",
                "French",
                "Georgian",
                "German",
                "Greek",
                "Gujarati",
                "Hebrew",
                "Hindi",
                "Hungarian",
                "Icelandic",
                "Indonesian",
                "Irish",
                "Italian",
                "Japanese",
                "Javanese",
                "Korean",
                "Latin",
                "Latvian",
                "Lithuanian",
                "Macedonian",
                "Malay",
                "Malayalam",
                "Maltese",
                "Maori",
                "Marathi",
                "Mongolian",
                "Nepali",
                "Norwegian",
                "Persian",
                "Polish",
                "Portuguese",
                "Punjabi",
                "Quechua",
                "Romanian",
                "Russian",
                "Samoan",
                "Serbian",
                "Slovak",
                "Slovenian",
                "Spanish",
                "Swahili",
                "Swedish",
                "Tamil",
                "Tatar",
                "Telugu",
                "Thai",
                "Tibetan",
                "Tonga",
                "Turkish",
                "Ukrainian",
                "Urdu",
                "Uzbek",
                "Vietnamese",
                "Welsh",
                "Xhosa"
            };
            if (Languages != null && !Languages.Any())
            {
                foreach (var language in languages)
                {
                    Languages.Add(new Language()
                    {
                        Name = language
                    });
                }
                SaveChanges();
            }

            if (Users != null && !Users.Any())
            {
                Users.Add(new User
                {
                    Username = "juanrondon"
                });
            }
            SaveChanges();
        }
    }
}