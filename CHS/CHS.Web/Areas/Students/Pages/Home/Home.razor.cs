using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Gym,Trainer", Policy = "FilledSurveyForm")]
    public partial class Home : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        string _name = string.Empty;
        string _role = string.Empty;
        private AuthenticationState _authenticationState;
        private HomeViewModel _model;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            _authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (_authenticationState.User.Identity.IsAuthenticated)
            {
                var response = await Service.SendRequest("Get", "Home");
                if (response.IsSuccessStatusCode)
                {
                    _model = await Service.Get<HomeViewModel>(response);
                }
                _role = _authenticationState.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

                _name = _authenticationState.User.Identity.Name;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("import", "/JavaScript.js");
            await JSRuntime.InvokeVoidAsync("window.Custom2");
            await JSRuntime.InvokeVoidAsync("window.Custom3");
            await JSRuntime.InvokeVoidAsync("window.DevNav");
            //await JSRuntime.InvokeVoidAsync("window.carousel");
            await JSRuntime.InvokeVoidAsync("window.LoadChart");
            await JSRuntime.InvokeVoidAsync("window.Dashboard1");
            await JSRuntime.InvokeVoidAsync("carouselReview");
        }

        [Parameter]
        public EventCallback<string> OnClick { get; set; }

    }
}
