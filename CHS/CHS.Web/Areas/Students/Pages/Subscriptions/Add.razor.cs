using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

using Newtonsoft.Json;

namespace CHS.Web.Areas.Students.Pages.Subscriptions
{
    [Authorize(Roles = "Trainer,Gym", Policy = "FilledSurveyForm")]
    public partial class Add : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        private string _role = string.Empty;
        private string _nameMessage = string.Empty;
        private bool _disable, _showDays;
        private SubscriptionViewModel _model = new SubscriptionViewModel();
        private ICollection<string> _selectedDays = new List<string>();
        private IDictionary<string, bool> _days = new Dictionary<string, bool>
        {
            { "Monday",false },
            { "Tuesday",false },
            { "Wednesday",false },
            { "Thursday" ,false},
            { "Friday" ,false},
            { "Saturday" ,false},
            { "Sunday" ,false},
        };

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var result = await LocalStorage.GetAsync<string>("roles");
            if (result.Success)
            {
                _role = result.Value;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("DeleteMdcRippleLine");
            }
        }

        private async Task HandleValidSubmit()
        {
            if (!_disable)
            {
                _disable = true;
                for (int i = 0; i < _selectedDays.Count; i++)
                {
                    _model.TrainingDays += "," + _selectedDays.ElementAt(i);
                }
                _model.TrainingDays.Trim(',');
                _model.TrainingDays.TrimStart(',');
                var response = await Service.SendRequest("Post", "Subscriptions", false, JsonConvert.SerializeObject(_model));
                if (response.IsSuccessStatusCode)
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
                var response = await Service.SendRequest("Get", $"Subscriptions/ValidateName?newName={_model.Name}");
                if (response.IsSuccessStatusCode)
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

        private void SelectDay(string day)
        {
            if (_selectedDays.Count < 4)
            {
                if (!_selectedDays.Any(x => x.Equals(day)))
                {
                    ToggleDaysList();
                    _selectedDays.Add(day);
                    var temp = _days.Where(x => x.Key.Equals(day)).FirstOrDefault();
                    _days.Remove(temp);
                    //_days.Add(day, true);
                }
            }
        }

        private void RemoveDay(string day)
        {
            _selectedDays.Remove(day);
            _days.Add(day, false);
        }

        private void ToggleDaysList()
        {
            if (_selectedDays.Count < 4)
            {
                _showDays = !_showDays;
            }
        }
    }
}
