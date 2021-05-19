using System;

namespace CHS.Infrastructure.ViewModels
{
    public class UserWithToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public Guid UserId { get; set; }
    }
}
