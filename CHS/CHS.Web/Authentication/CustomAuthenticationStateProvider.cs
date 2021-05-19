using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CHS.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var emailAddress = await _sessionStorageService.GetItemAsync<string>("emailAddress");
            var token = await _localStorage.GetAsync<string>("token");
            ClaimsIdentity identity;

            if (token.Success)
            {
                var name = await _localStorage.GetAsync<string>("name");
                var roles = await _localStorage.GetAsync<string>("roles");
                //var id = await _localStorage.GetAsync<string>("id");
                var all = await _localStorage.GetAsync<string>("all");
                //var filledSurveyForm = await _localStorage.GetAsync<string>("FilledSurveyForm");
                if (all.Success)
                {
                    var arr = all.Value.Split(',');
                    var claims = new Claim[arr.Length + 3];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        var result = await _localStorage.GetAsync<string>(arr[i]);
                        if (result.Success)
                        {
                            claims[i] = new Claim(arr[i], result.Value);
                        }
                    }
                    claims[^3] = new Claim("all", all.Value);
                    claims[^2] = new Claim(ClaimTypes.Name, name.Value ?? "");
                    claims[^1] = new Claim(ClaimTypes.Role, roles.Value ?? "");
                    identity = new ClaimsIdentity(claims, "apiauth_type");
                }
                else
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, name.Value ?? ""),
                        new Claim(ClaimTypes.Role, roles.Value ?? "")
                    }, "apiauth_type");

                }
                //if (filledSurveyForm.Success)
                //{
                //    var logoImage = await _localStorage.GetAsync<string>("LogoImage");
                //    identity = new ClaimsIdentity(new[]
                //    {
                //        new Claim(ClaimTypes.Name, name.Value ?? ""),
                //        new Claim(ClaimTypes.Role, roles.Value ?? ""),
                //        //new Claim("LogoImage", logoImage ?? ""),
                //        new Claim("FilledSurveyForm", filledSurveyForm.Value)
                //    }, "apiauth_type");
                //}
                //else
                //{
                //    identity = new ClaimsIdentity(new[]
                //    {
                //            new Claim(ClaimTypes.Name, name.Value ?? ""),
                //            new Claim(ClaimTypes.Role, roles.Value ?? "")
                //    }, "apiauth_type");
                //}
            }
            else
            {
                identity = new ClaimsIdentity();
            }
            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(string emailAddress, string role, string filledSurveyForm)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, emailAddress),
                new Claim(ClaimTypes.Role, role),
                new Claim("FilledSurveyForm", filledSurveyForm ?? ""),
                //new Claim("LogoImage", logoImage ?? "")

            }, "apiauth_type");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsNotAuthenticated()
        {
            //await _sessionStorageService.RemoveItemAsync("emailAddress");
            await _localStorage.DeleteAsync("all");
            await _localStorage.DeleteAsync("token");
            await _localStorage.DeleteAsync("roles");
            await _localStorage.DeleteAsync("name");
            await _localStorage.DeleteAsync("id");
            await _localStorage.DeleteAsync("memberid");
            await _localStorage.DeleteAsync("FilledSurveyForm");
            await _localStorage.DeleteAsync("LogoImage");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }

}
