using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class PostLike : Common
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        /// <summary>
        /// Person who liked the post.
        /// </summary>
        //[Column(nameof(Member))]
        //public Guid TrainerId { get; set; }
        //public Member Member { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
