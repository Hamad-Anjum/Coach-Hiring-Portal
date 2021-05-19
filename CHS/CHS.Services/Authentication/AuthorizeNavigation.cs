using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CHS.Services.Authentication
{
    public class AuthorizeNavigation
    {
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authentication;

        public AuthorizeNavigation(NavigationManager navigationManager, AuthenticationStateProvider authentication)
        {
            _navigationManager = navigationManager;
            _authentication = authentication;
        }

        public async Task NavigateToLogin(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var auth = await _authentication.GetAuthenticationStateAsync();
                if (!auth.User.Identity.IsAuthenticated)
                {
                    _navigationManager.NavigateTo($"/Account/Login?returnUrl={returnUrl}");
                }
            }
        }
    }
}
