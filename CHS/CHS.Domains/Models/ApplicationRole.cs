
using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace CHS.Domains.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public string Description { get; set; }
        public int Index { get; set; } = 0;
        public bool IsEnable { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
