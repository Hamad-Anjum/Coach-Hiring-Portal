using System;

namespace CHS.Infrastructure.ViewModels
{
    public class PostImageViewModel
    {
        public Guid PostId { get; set; }
        public string Picture { get; set; }
        public string ContentType { get; set; }
        public int SortOrder { get; set; }
    }
}
