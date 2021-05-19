using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class GymMember : Common
    {
        public Guid GymId { get; set; }
        public Gym Gym { get; set; }

        //[Column(nameof(Member))]
        //public Guid TrainerId { get; set; }
        //public Member Member { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
