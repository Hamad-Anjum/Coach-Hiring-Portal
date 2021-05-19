using System.Collections.Generic;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Trainer,Gym", Policy = "FilledSurveyForm")]
    public partial class Subscription : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        private ICollection<SubscriptionViewModel> _models;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var response = await Service.SendRequest("Get", "Subscriptions/GetAllByOwners");
            if (response != null)
            {
                _models = await Service.GetAll<SubscriptionViewModel>(response);
            }
        }
    }
}
