using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Students.Pages.Subscriptions
{
    [Authorize(Roles = "Trainer,Gym", Policy = "FilledSurveyForm")]
    public partial class List : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private ICollection<SubscriptionViewModel> _models;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            await Get();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeAsync<object>("removeCssClass", "matTbl");
        }

        async Task Get()
        {
            var response = await Service.SendRequest("Get", "Subscriptions/GetAllByOwners");
            if (response != null)
            {
                _models = await Service.GetAll<SubscriptionViewModel>(response);
            }
        }

        private async Task ActiveToggle(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Subscriptions/ActiveToggle?id={id}");
            if (response != null)
            {
                var responseMessage = await Service.Get<HttpStatusCode>(response);
                if (responseMessage == HttpStatusCode.OK)
                {
                    await Get();
                }
            }
        }

        private async Task Delete(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Subscriptions/{id}");
            if (response != null)
            {
                var responseMessage = await Service.Get<HttpStatusCode>(response);
                if (responseMessage == HttpStatusCode.OK)
                {
                    await Get();
                }
            }
        }
    }
}
