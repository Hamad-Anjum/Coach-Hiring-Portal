using System;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Common.Pages
{
    [Authorize(Roles = "Student")]
    public partial class BasicInfo : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private BasicInfoViewModel _member;
        private string _role;

        protected override void OnInitialized()
        {
            //var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //var response = await Service.SendRequest("Get", $"Profile/GetUserDetails?name={user.User.Identity.Name}");
            //if (response != null)
            //{
            //    _member = await Service.Get<ProfileViewModel>(response);

            //}
        }

        private void HandleValidSubmit()
        {

        }
    }
}
