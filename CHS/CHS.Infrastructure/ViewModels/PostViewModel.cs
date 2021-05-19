using System;
using System.Collections.Generic;

namespace CHS.Infrastructure.ViewModels
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Likes { get; set; }
        public bool LikedThisPost { get; set; }
        public bool InEditMode { get; set; }
        public ICollection<string> Replies { get; set; }
        public DateTime DateTime { get; set; }
    }
}
