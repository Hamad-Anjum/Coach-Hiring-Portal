using System;

using Microsoft.AspNetCore.Identity;

namespace CHS.Domains.Models
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationRole Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {

    }

    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {

    }
    
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {

    }

    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {

    }
}
