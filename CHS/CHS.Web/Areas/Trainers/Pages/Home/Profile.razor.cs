using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Trainers.Pages.Home
{
    public partial class Profile : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        //private Member _member;
        private string _role;

        protected override async Task OnInitializedAsync()
        {
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            _role = user.User.Claims.FirstOrDefault(x => x.Value == ClaimTypes.Role).Value;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //await JSRuntime.InvokeVoidAsync("window.Custom2");
            //await JSRuntime.InvokeVoidAsync("window.Custom3");
            //await JSRuntime.InvokeVoidAsync("window.DevNav");
            ////await JSRuntime.InvokeVoidAsync("window.carousel");
            //await JSRuntime.InvokeVoidAsync("window.Dashboard1");
            //await JSRuntime.InvokeVoidAsync("carouselReview");
            //await JSRuntime.InvokeVoidAsync("window.LoadChart");
            ////await JSRuntime.InvokeVoidAsync("Athlete/js/parallax/jquery.transform2d.js");
        }

        private async Task HandleValidSubmit()
        {

        }
    }
}
