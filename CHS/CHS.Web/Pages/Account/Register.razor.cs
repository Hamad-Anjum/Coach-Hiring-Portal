
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.Authentication;
using CHS.Services.IService;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CHS.Web.Pages.Account
{
    public partial class Register : ComponentBase
    {
        [Inject]
        public ProtectedLocalStorage ProtectedLocalStorage { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private RegisterViewModel _register = new RegisterViewModel();
        private bool _disable;
        private string _emailMessage = string.Empty;
        private string _usernameMessage = string.Empty;
        private string _message = string.Empty;
        private string _roleMessage = string.Empty;

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        public async Task HandleValidSubmit()
        {
            if (ValidateRole())
            {
                Disabled();
                var responseStatusCode = await UserService.RegisterAsync(model: _register);

                if (!string.IsNullOrEmpty(responseStatusCode.AccessToken))
                {
                    await ProtectedLocalStorage.SetAsync("token", responseStatusCode.AccessToken);
                    await ProtectedLocalStorage.SetAsync("name", responseStatusCode.UserName);
                    await ProtectedLocalStorage.SetAsync("roles", responseStatusCode.Role);
                    await ProtectedLocalStorage.SetAsync("id", responseStatusCode.Id);
                    if (responseStatusCode.FilledSurveyForm)
                    {
                        await ProtectedLocalStorage.SetAsync("FilledSurveyForm", responseStatusCode.FilledSurveyForm.ToString());
                    }
                    ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(responseStatusCode.UserName, responseStatusCode.Role, null);
                    NavigationManager.NavigateTo($"/Survey/{responseStatusCode.Id}");
                }
                else
                {
                    _disable = false;
                    _message = $"Invalid Login Attempt";
                }
            }
        }

        private async Task CheckUserName()
        {
            var response = await Service.SendRequest("Get", $"Account/CheckUsername?name={_register.UserName}");
            if (response != null)
            {
                if (await Service.Get<bool>(response))
                {
                    _usernameMessage = string.Empty;
                }
                else
                {
                    _usernameMessage = $"Username {_register.UserName} already Exist.";
                }
            }
        }

        private async Task CheckEmail()
        {
            var response = await Service.SendRequest("Get", $"Account/CheckEmail?email={_register.UserName}");
            if (response != null)
            {
                if (await Service.Get<bool>(response))
                {
                    _emailMessage = string.Empty;
                }
                else
                {
                    _emailMessage = $"Email {_register.Email} already Exist.";
                }
            }
        }

        private bool ValidateRole()
        {
            if (string.IsNullOrEmpty(_register.Role))
            {
                _roleMessage = "Role is Required";
                return false;
            }
            _roleMessage = string.Empty;
            return true;
        }

        private void Disabled()
        {
            _disable = true;
        }
    }
}
