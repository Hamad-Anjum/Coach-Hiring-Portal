using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Newtonsoft.Json;

namespace CHS.Web.Areas.Students.Pages.Subscriptions
{
    [Authorize(Roles = "Trainer,Gym", Policy = "FilledSurveyForm")]
    public partial class Edit : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private string _nameMessage = string.Empty, _role = string.Empty, _currentName = string.Empty;
        private bool _disable;
        private SubscriptionViewModel _model;
        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();

            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (user.User.Identity.IsAuthenticated)
            {
                _role = user.User.Claims.Where(x => x.Type.Equals("roles")).FirstOrDefault().Value;
            }
            var response = await Service.SendRequest("Get", $"Subscriptions/{Id}");
            if (response != null)
            {
                _model = await Service.Get<SubscriptionViewModel>(response);
                _currentName = _model.Name;
            }
        }

        private async Task HandleValidSubmit()
        {
            if (!_disable)
            {
                var response = await Service.SendRequest("Put", "Subscriptions/" + Id, false, JsonConvert.SerializeObject(_model));
                if (response != null)
                {
                    var responseMessage = await Service.Get<HttpStatusCode>(response);
                    if (responseMessage == HttpStatusCode.OK)
                    {
                        NavigationManager.NavigateTo("/Subscriptions/List");
                    }
                }
                else
                {

                }
            }
        }

        private async Task ValidateName()
        {
            if (!string.IsNullOrEmpty(_model.Name))
            {
                var response = await Service.SendRequest("Get", $"Subscriptions/ValidateNameInEdit?currentName={_currentName}&newName={_model.Name}");
                if (response != null)
                {
                    bool result = await Service.Get<bool>(response);
                    if (result)
                    {
                        _nameMessage = $"Name {_model.Name} already Exist";
                        _disable = true;
                    }
                    else
                    {
                        _nameMessage = string.Empty;
                        _disable = false;
                    }
                }
                else
                {
                    _nameMessage = "Sorry, Something went wrong. Please try again Later";
                    _disable = true;
                }
            }
        }
    }
}
