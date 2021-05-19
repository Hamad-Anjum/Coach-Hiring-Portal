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

namespace CHS.Web.Areas.Admin.Page.District
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

        DistrictViewModel _model = new DistrictViewModel();
        ICollection<DistrictViewModel> _districts;
        ICollection<CityViewModel> _cities;
        ICollection<StateViewModel> _states;
        ICollection<CountryViewModel> _countries;
        CountryStateCityDistrict _countryStateCityDistrict;
        private bool _disable;
        private string _districtMessage = string.Empty;
        private string _cityMessage = string.Empty;
        private string _stateMessage = string.Empty;
        private string _countryMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var response = await Service.SendRequest("Get", "Districts/CountryStateCityDistrict");
            if (response.IsSuccessStatusCode)
            {
                _countryStateCityDistrict = await Service.Get<CountryStateCityDistrict>(response);
                _districts = _countryStateCityDistrict.Districts;
                //_cities = _countryStateCityDistrict.Cities;
                //_states = _countryStateCityDistrict.States;
                _countries = _countryStateCityDistrict.Countries;
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

        private async Task GetCitiesByState(Guid e)
        {
            var response = await Service.SendRequest("Get", $"Cities/GetCitiesByState?stateId={e}");
            if (response != null)
            {
                _cities = await Service.GetAll<CityViewModel>(response);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeAsync<object>("removeCssClass", "matTbl");
        }

        async Task GetDistricts()
        {
            var response = await Service.SendRequest("Get", "Districts");
            if (response != null)
            {
                _districts = await Service.GetAll<DistrictViewModel>(response);
                _districts = _districts.OrderBy(x => x.Name).ToList();
            }
        }

        void SetDistrict(string e)
        {
            if (string.IsNullOrEmpty(e))
            {
                _model.Name = string.Empty;
            }
            else
            {
                _model.Name = e;
            }
        }

        private async Task AddDistrictWithEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await AddNewDistrict();
            }
        }

        void SetCity(Guid e)
        {
            if (e == default)
            {
                _model.CityId = default;
                _cityMessage = "City is Required";
            }
            else
            {
                _model.CityId = e;
                _cityMessage = string.Empty;
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

        async Task SetState(Guid e)
        {
            if (e == default)
            {
                _model.StateId = default;
                _stateMessage = "State is Required";
            }
            else
            {
                _model.StateId = e;
                await GetCitiesByState(e);
                _stateMessage = string.Empty;
            }
        }

        private async Task AddNewDistrict()
        {
            _disable = true;
            if (_disable && ValidateDropDown())
            {
                var response = await Service.SendRequest("Post", $"Districts", false, JsonConvert.SerializeObject(_model));
                if (response != null)
                {
                    var responseMessage = await Service.Post(response);
                    if (responseMessage == HttpStatusCode.OK)
                    {
                        await GetDistricts();
                        _districtMessage = string.Empty;
                    }
                    else
                    {
                        _districtMessage = $"District '{_model.Name}' Cannot be added";
                    }
                }
                else
                {
                    _districtMessage = $"District {_model.Name} Cannot be added";
                }
                _model = new DistrictViewModel();
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
            if (_model.CityId == default)
            {
                _cityMessage = "City is Required";
            }
            else
            {
                _cityMessage = string.Empty;
            }
            if (_model.CountryId == default || _model.StateId == default || _model.CityId == default)
            {
                return false;
            }
            return true;
        }

        async Task ActiveToggle(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Districts/ActiveToggle?id={id}");
            if (response != null)
            {
                await GetDistricts();
            }
        }

        async Task Delete(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Districts/{id}");
            if (response != null)
            {
                await GetDistricts();
            }
        }
    }
}
