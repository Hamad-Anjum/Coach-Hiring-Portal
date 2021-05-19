using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Following : Common
    {
        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(FolloweeUser))]
        public Guid FolloweeId { get; set; }

        public ApplicationUser FolloweeUser { get; set; }
    }
}
