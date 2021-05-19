using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

using Newtonsoft.Json;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Trainer,Gym", Policy = "FilledSurveyForm")]
    public partial class BasicInfo : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public IHttpService Service { get; set; }

        private ICollection<DistrictViewModel> _districts;
        private ICollection<CityViewModel> _cities;
        private ICollection<StateViewModel> _states;
        private ICollection<CountryViewModel> _countries;
        private BasicInfoViewModel _member;
        private string _id = string.Empty;
        private string _role = string.Empty;
        private string _name = string.Empty;
        private bool _processing = false;

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

                var response = await Service.SendRequest("Get", $"BasicInfo");
                if (response.IsSuccessStatusCode)
                {
                    _member = await Service.Get<BasicInfoViewModel>(response);
                    if (_member != null)
                    {
                        _member.Password = "1";
                        _member.ReTypePassword = "1";
                        await GetCountries();
                        await GetStates();
                        await GetCities();
                        await GetDistricts();
                    }
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                await JSRuntime.InvokeVoidAsync("import", "/JavaScript.js");
                await JSRuntime.InvokeVoidAsync("window.Custom2");
                await JSRuntime.InvokeVoidAsync("window.Custom3");
                await JSRuntime.InvokeVoidAsync("window.DevNav");
                //await JSRuntime.InvokeVoidAsync("window.carousel");
                await JSRuntime.InvokeVoidAsync("window.LoadChart");
                await JSRuntime.InvokeVoidAsync("window.Dashboard1");
                await JSRuntime.InvokeVoidAsync("carouselReview");
                await JSRuntime.InvokeVoidAsync("DeleteMdcRippleLine");
            }
        }

        async Task GetCountries()
        {
            var response = await Service.SendRequest("Get", $"Countries/GetActive");
            if (response != null)
            {
                _countries = await Service.GetAll<CountryViewModel>(response);
            }
        }

        async Task GetStates()
        {
            var response = await Service.SendRequest("Get", $"States/GetStatesByCountry?countryId={_member.CountryId}");
            if (response != null)
            {
                _states = await Service.GetAll<StateViewModel>(response);
            }
        }

        async Task GetCities()
        {
            var response = await Service.SendRequest("Get", $"Cities/GetCitiesByState?stateId={_member.StateId}");
            if (response != null)
            {
                _cities = await Service.GetAll<CityViewModel>(response);
            }
        }

        async Task GetDistricts()
        {
            var response = await Service.SendRequest("Get", $"Districts/GetDistrictsByCity?cityId={_member.CityId}");
            if (response != null)
            {
                _districts = await Service.GetAll<DistrictViewModel>(response);
            }
        }

        private async Task SetCountry(Guid e)
        {
            _member.CountryId = e;
            _member.StateId = default;
            _member.CityId = default;
            _member.DistrictId = default;
            if (_member.CountryId != default)
            {
                await GetStates();
                _cities = new List<CityViewModel>();
                _districts = new List<DistrictViewModel>();
            }
        }

        private async Task SetState(Guid e)
        {
            _member.StateId = e;
            _member.CityId = default;
            _member.DistrictId = default;
            if (_member.StateId != default)
            {
                await GetCities();
                _districts = new List<DistrictViewModel>();
            }
        }

        private async Task SetCity(Guid e)
        {
            _member.CityId = e;
            _member.DistrictId = default;
            if (_member.CityId != default)
            {
                await GetDistricts();
            }
        }

        private async Task SetDistricts(Guid e)
        {
            _member.DistrictId = e;
            if (_member.DistrictId != default)
            {

            }
        }

        private async Task HandleValidSubmit()
        {
            if (_member.MemberId != default)
            {
                _processing = true;
                var response = await Service.SendRequest("Put", $"BasicInfo/{_member.MemberId}", false, JsonConvert.SerializeObject(_member));
                if (response != null)
                {
                    var responseMessage = await Service.Put(response);
                    if (responseMessage == HttpStatusCode.OK)
                    {
                        await JSRuntime.InvokeVoidAsync("SavedSuccessFully");
                    }
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("NotSavedSuccessFully");
                }
            }
            _processing = false;
        }
    }
}
