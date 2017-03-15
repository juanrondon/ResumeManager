using System.Collections.Generic;

namespace ResumeManager.Commands.ResumeDraft
{
    public class UpdateResumeDraftCommand
    {
        public int Id { get; set; }

        public byte[] ProfilePhoto { get; set; }

        public string ProfilePhotoFileType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string GitHub { get; set; }

        public string LinkedIn { get; set; }

        public List<int> ResumeDraftLanguagesIds { get; set; }

        public string PersonalSkills { get; set; }

        public string References { get; set; }

        public int UserId { get; set; }        
    }
}
