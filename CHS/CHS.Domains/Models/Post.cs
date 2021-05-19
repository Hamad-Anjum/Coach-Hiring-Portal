using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Post : Common
    {
        public string Title { get; set; }
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<PostComment> PostComments { get; set; }
        public ICollection<PostImage> PostImages { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostVideo> PostVideos { get; set; }
    }
}
