using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class PostUploadViewModel
    {
        public Guid PostId { get; set; } = Guid.NewGuid();

        [Required]
        public string Text { get; set; }

        public string Description { get; set; }

        [Required]
        public string File { get; set; }

        public string UserId { get; set; }

        public byte[] ImageBytes { get; set; }

        public ICollection<PostImageViewModel> PostImages { get; set; } = new List<PostImageViewModel>();

        //public MultipartFormDataContent Content { get; set; }
    }
}
