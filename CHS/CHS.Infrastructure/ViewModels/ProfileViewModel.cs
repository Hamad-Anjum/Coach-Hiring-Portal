using System;
using System.Collections.Generic;

namespace CHS.Infrastructure.ViewModels
{
    public class ProfileViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string ProfilePicture { get; set; }
        public string CoverPicture { get; set; }
        public bool IsFollowThisUser { get; set; }
        public int Followers { get; set; }
        public int Followings { get; set; }
        public ICollection<PostViewModel> Posts { get; set; }
    }
}
