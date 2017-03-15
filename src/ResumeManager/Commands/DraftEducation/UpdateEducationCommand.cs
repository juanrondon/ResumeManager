using System;

namespace ResumeManager.Commands.DraftEducation
{
    public class UpdateEducationCommand
    {
        public int DraftEducationId { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string Grade { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public string Description { get; set; }
    }
}