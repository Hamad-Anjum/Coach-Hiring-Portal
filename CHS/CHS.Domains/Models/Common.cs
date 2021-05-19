using System;

namespace CHS.Domains.Models
{
    public class Common
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public int Index { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
