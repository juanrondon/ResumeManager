using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Commands.Resume
{
    public class SaveContactDetailsCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public List<int> LanguageListIds { get; set; }
        public string GitHub { get; set; }
        public string LinkedIn { get; set; }
    }
}
