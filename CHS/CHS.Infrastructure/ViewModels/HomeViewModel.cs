using System.Collections.Generic;

namespace CHS.Infrastructure.ViewModels
{
    public class HomeViewModel
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public string Designation { get; set; }
        public ICollection<MemberSkillViewModel> MemberSkills { get; set; }
        public string ProfilePicture { get; set; }
    }
}
