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

namespace CHS.Web.Areas.Admin.Page.City
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

        CityViewModel _model = new CityViewModel();
        ICollection<CityViewModel> _cities;
        ICollection<StateViewModel> _states;
        ICollection<CountryViewModel> _countries;
        CountryStateCity _stateAndCity;
        private bool _disable;
        private string _cityMessage = string.Empty;
        private string _stateMessage = string.Empty;
        private string _countryMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var response = await Service.SendRequest("Get", "Cities/CountryStateCity");
            if (response != null)
            {
                _stateAndCity = await Service.Get<CountryStateCity>(response);
                _cities = _stateAndCity.Cities;
                //_states = _stateAndCity.States;
                _countries = _stateAndCity.Countries;
            }
        }

        private async Task GetStatesByCountry(Guid e)
        {
            var response = await Service.SendRequest("Get", $"States/GetStatesByCountry?countryId={e}");
            if (response != null)
            {
                _states = await Service.GetAll<StateViewModel>(response);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeAsync<object>("removeCssClass", "matTbl");
        }

        async Task GetCities()
        {
            var response = await Service.SendRequest("Get", "Cities");
            if (response != null)
            {
                _cities = await Service.GetAll<CityViewModel>(response);
                _cities = _cities.OrderBy(x => x.CityName).ToList();
            }
        }

        void SetCity(string e)
        {
            if (string.IsNullOrEmpty(e))
            {
                _model.CityName = string.Empty;
            }
            else
            {
                _model.CityName = e;
            }
        }

        private async Task AddCityWithEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await AddNewCity();
            }
        }

        async Task SetCountry(Guid e)
        {
            if (e == default)
            {
                _model.CountryId = default;
                _countryMessage = "Country is Required";
            }
            else
            {
                _model.CountryId = e;
                await GetStatesByCountry(e);
                _countryMessage = string.Empty;
            }
        }

        void SetState(Guid e)
        {
            if (e == default)
            {
                _model.StateId = default;
                _stateMessage = "State is Required";
            }
            else
            {
                _model.StateId = e;
                _stateMessage = string.Empty;
            }
        }

        private async Task AddNewCity()
        {
            _disable = true;
            if (_disable && ValidateDropDown())
            {
                var response = await Service.SendRequest("Post", $"Cities", false, JsonConvert.SerializeObject(_model));
                if (response != null)
                {
                    var responseMessage = await Service.Post(response);
                    if (responseMessage == HttpStatusCode.OK)
                    {
                        await GetCities();
                        _cityMessage = string.Empty;
                    }
                    else
                    {
                        _cityMessage = $"City '{_model.CityName}' Cannot be added";
                    }
                }
                else
                {
                    _cityMessage = $"City {_model.CityName} Cannot be added";
                }
                _model.CityName = string.Empty;
            }
            _disable = false;
        }

        bool ValidateDropDown()
        {
            if (_model.CountryId == default)
            {
                _countryMessage = "Country is Required";
            }
            else
            {
                _countryMessage = string.Empty;
            }
            if (_model.StateId == default)
            {
                _stateMessage = "State is Required";
            }
            else
            {
                _stateMessage = string.Empty;
            }
            if (_model.CountryId == default || _model.StateId == default)
            {
                return false;
            }
            return true;
        }

        async Task Delete(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Cities/{id}");
            if (response != null)
            {
                await GetCities();
            }
        }
    }
}
