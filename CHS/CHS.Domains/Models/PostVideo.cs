using System;

namespace CHS.Domains.Models
{
    public class PostVideo : Common
    {
        public string Video { get; set; }
        public string ContentType { get; set; }
        public int SortOrder { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
