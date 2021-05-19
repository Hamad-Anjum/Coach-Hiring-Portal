using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

using Newtonsoft.Json;

namespace CHS.Web.Areas.Admin.Page.State
{
    [Authorize]
    public partial class List : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        ICollection<StateViewModel> _states;
        ICollection<CountryViewModel> _countries;
        private CountryAndState _countryState;
        StateViewModel _model = new StateViewModel();
        private bool _disable;
        private string _stateMessage = string.Empty;
        private string _countryMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var response = await Service.SendRequest("Get", "States/CountryAndState");
            if (response != null)
            {
                _countryState = await Service.Get<CountryAndState>(response);
                _states = _countryState.States;
                _countries = _countryState.Countries;
            }
        }

        async Task GetStates()
        {
            var response = await Service.SendRequest("Get", "States");
            if (response != null)
            {
                _states = await Service.GetAll<StateViewModel>(response);
                _states = _states.OrderBy(x => x.StateName).ToList();
            }
        }

        void SetCountry(Guid e)
        {
            if (e == default)
            {
                _model.CountryId = default;
                _countryMessage = "Country is Required";
            }
            else
            {
                _model.CountryId = e;
                _countryMessage = string.Empty;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeAsync<object>("removeCssClass", "matTbl");
        }

        private void HandleValidSubmit()
        {
            _disable = true;
        }

        private async Task AddStateWithEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await AddNewState();
            }
        }

        void SetState(string e)
        {
            if (string.IsNullOrEmpty(e))
            {
                _model.StateName = string.Empty;
            }
            else
            {
                _model.StateName = e;
            }
        }

        private async Task AddNewState()
        {
            _disable = true;
            if (_disable)
            {
                if (ValidateDropdown())
                {
                    var response = await Service.SendRequest("Post", $"States", false, JsonConvert.SerializeObject(_model));
                    if (response != null)
                    {
                        var responseMessage = await Service.Post(response);
                        if (responseMessage == HttpStatusCode.OK)
                        {
                            await GetStates();
                            _stateMessage = string.Empty;
                        }
                        else
                        {
                            _stateMessage = $"State '{_model.StateName}' Cannot be added";
                        }
                    }
                    else
                    {
                        _stateMessage = $"State {_model.StateName} Cannot be added";
                    }
                    _model.StateName = string.Empty;
                }
                _disable = false;
            }
        }

        bool ValidateDropdown()
        {
            if (_model.CountryId == default)
            {
                _countryMessage = "Country is Required";
                return false;
            }
            _countryMessage = string.Empty;
            return true;
        }

        async Task Delete(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Countries/{id}");
            if (response != null)
            {
                await GetStates();
            }
        }
    }
}
