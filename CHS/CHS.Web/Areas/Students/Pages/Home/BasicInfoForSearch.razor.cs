using System;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Gym,Trainer", Policy = "FilledSurveyForm")]
    public partial class BasicInfoForSearch : ComponentBase
    {
        [Parameter]
        public Guid? Id { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        private BasicInfoViewModel _member;
        private string _role = string.Empty;
        private string _name = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var val = await LocalStorage.GetAsync<string>("name");
            if (val.Success)
            {
                _name = val.Value;
            }

            val = await LocalStorage.GetAsync<string>("roles");
            if (val.Success)
            {
                _role = val.Value;
            }

            var response = await Service.SendRequest("Get", $"BasicInfo/{Id}");
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    _member = await Service.Get<BasicInfoViewModel>(response);
                    _member.Password = "1";
                    _member.ReTypePassword = "1";
                }
            }
        }
    }
}
