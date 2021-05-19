
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CHS.Web.Pages
{
    public partial class RedirectToLogin : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //string temp = NavigationManager.Uri.Split("https://localhost:44356")[1];
            //if (string.IsNullOrEmpty(temp) || temp == "/")
            //{
            //NavigationManager.NavigateTo($"/Login");
            //}
            //else
            //{
            //    NavigationManager.NavigateTo($"/Account/login?returnUrl={temp}");
            //}

            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (user.User.Identity is null || !user.User.Identity.IsAuthenticated)
            {
                var Baseurl = new Uri(NavigationManager.Uri);
                var host = Baseurl.GetLeftPart(UriPartial.Authority);
                string url = NavigationManager.Uri.Split(host + "/")[1];
                if (string.IsNullOrEmpty(url))
                {
                    NavigationManager.NavigateTo($"/Login");
                }
                else
                {
                    if (NavigationManager.Uri.Split('/').Any(x => x.Equals("Survey")))
                    {
                        NavigationManager.NavigateTo($"/Login");
                    }
                    else
                    {
                        NavigationManager.NavigateTo($"/Login?returnUrl={url}");
                    }
                }
            }
            else
            {
                NavigationManager.NavigateTo($"/Login");
            }
        }
    }
}
