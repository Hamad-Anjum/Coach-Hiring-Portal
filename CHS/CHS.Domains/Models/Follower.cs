using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Follower : Common
    {
        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(FollowerUser))]
        public Guid FollowerId { get; set; }

        public ApplicationUser FollowerUser { get; set; }
    }
}
