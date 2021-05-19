using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.Authentication;
using CHS.Services.IService;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

namespace CHS.Web.Pages.Account
{
    public partial class Login : ComponentBase
    {
        [Parameter]
        public string ReturnUrl { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ProtectedLocalStorage ProtectedLocalStorage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        private LoginViewModel _login = new LoginViewModel();

        private string _temp = string.Empty;
        private bool _processing;
        protected override async Task OnInitializedAsync()
        {
            _processing = true;
            if (NavigationManager.Uri.Contains("returnUrl="))
            {
                _temp = NavigationManager.Uri.Split("returnUrl=")[1];
            }

            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (user.User.Identity.IsAuthenticated) // you can also check roles here.
            {
                if (user.User.Claims.Any(x => x.Type.Equals("all") && x.Value.Contains("FilledSurveyForm"))
                    && !user.User.Claims.Any(x => x.Type.Equals("FilledSurveyForm")))
                {
                    await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsNotAuthenticated();
                }
                else if ((user.User.Claims.Any(x => (x.Type.Equals("all") && !x.Value.Contains("FilledSurveyForm")))
                    && !user.User.Claims.Any(x => x.Type.Equals("FilledSurveyForm")))
                    || (!user.User.Claims.Any(x => x.Type.Equals("all"))))
                {
                    var result = await ProtectedLocalStorage.GetAsync<string>("id");
                    if (result.Success)
                    {
                        NavigationManager.NavigateTo("Survey/" + result.Value);
                    }
                    else
                    {
                        await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsNotAuthenticated();
                    }
                }
                else
                {
                    if (user.User.IsInRole("Admin"))
                        NavigationManager.NavigateTo($"{_temp ?? "/Admin/Home"}");
                    else if (user.User.IsInRole("Student") || user.User.IsInRole("Gym") || user.User.IsInRole("Trainer"))
                        NavigationManager.NavigateTo($"{_temp ?? "/Home"}");
                }
                //else if (user.User.IsInRole("Trainer"))
                //    NavigationManager.NavigateTo($"{_temp ?? "/Trainer/Home"}");
                //else if (user.User.IsInRole("Gym"))
                //    NavigationManager.NavigateTo($"{_temp ?? "/Gym/Home"}");
            }
            _processing = false;
        }

        private string _message = string.Empty;

        private async Task HandleValidSubmit()
        {
            Disabled();
            var result = await UserService.LoginAsync(model: _login);

            if (!string.IsNullOrEmpty(result.AccessToken) && !string.IsNullOrEmpty(result.Email))
            {
                //await LocalStorageService.SetItemAsync("refreshToken", result.RefreshToken);
                await ProtectedLocalStorage.SetAsync("token", result.AccessToken);
                await ProtectedLocalStorage.SetAsync("name", result.Email);
                await ProtectedLocalStorage.SetAsync("roles", result.Role);
                await ProtectedLocalStorage.SetAsync("id", result.Id);
                await ProtectedLocalStorage.SetAsync("memberid", result.MemberId);
                if (result.FilledSurveyForm || result.Role.Equals("Admin"))
                {
                    await ProtectedLocalStorage.SetAsync("FilledSurveyForm", "True");
                    await ProtectedLocalStorage.SetAsync($"all", $"memberid,id,FilledSurveyForm");
                }
                else
                {
                    await ProtectedLocalStorage.SetAsync($"all", $"memberid,id");
                }

                ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(result.Email, result.Role, result.FilledSurveyForm.ToString());
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

                if (result.FilledSurveyForm && result.Role.Equals("Admin"))
                {
                    NavigationManager.NavigateTo("/District");
                }
                else if (result.FilledSurveyForm)
                {
                    if (NavigationManager.Uri.Contains("returnUrl="))
                    {
                        _temp = NavigationManager.Uri.Split("returnUrl=")[1];
                    }

                    if (NavigationManager.Uri.Contains("/Login"))
                    {
                        if (result.Role == "Student" || result.Role == "Gym" || result.Role == "Trainer")
                            NavigationManager.NavigateTo($"{_temp ?? "/Home"}");
                    }
                }
                else
                {
                    NavigationManager.NavigateTo($"/Survey/{result.Id}");
                }
            }
            else
            {
                _disable = false;
                _message = $"Invalid Login Attempt";
            }
        }

        private bool _disable;

        private void Disabled()
        {
            _disable = true;
        }
    }
}
