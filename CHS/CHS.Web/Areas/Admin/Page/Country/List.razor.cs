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

namespace CHS.Web.Areas.Admin.Page.Country
{
    [Authorize(Roles = "Admin", Policy = "FilledSurveyForm")]
    public partial class List : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        ICollection<CountryViewModel> _countries;
        CountryViewModel _model = new CountryViewModel();
        private bool _disable;
        private string _countryMessage = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            await GetCountries();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeAsync<object>("removeCssClass", "matTbl");
        }

        private async Task GetCountries()
        {
            var response = await Service.SendRequest("get", "Countries");
            if (response != null)
            {
                _countries = await Service.GetAll<CountryViewModel>(response);
                _countries = _countries.OrderBy(x => x.Name).ToList();
            }
        }

        private void HandleValidSubmit()
        {
            _disable = true;
        }

        private async Task AddCountryWithEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await AddNewCountry();
            }
        }

        void SetCountry(string e)
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

        private async Task AddNewCountry()
        {
            _disable = true;
            if (_disable)
            {
                if (string.IsNullOrEmpty(_model.Name))
                {

                }
                else
                {
                    var response = await Service.SendRequest("Post", $"Countries", false, JsonConvert.SerializeObject(_model));
                    if (response != null)
                    {
                        var responseMessage = await Service.Post(response);
                        if (responseMessage == HttpStatusCode.OK)
                        {
                            await GetCountries();
                            _countryMessage = string.Empty;
                        }
                        else
                        {
                            _countryMessage = $"Country '{_model.Name}' Cannot be added";
                        }
                    }
                    else
                    {
                        _countryMessage = $"Country '{_model.Name}' Cannot be added";
                    }
                    _model.Name = string.Empty;
                }
            }
            _disable = false;
        }

        async Task Delete(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Countries/ActiveToggle?id={id}");
            if (response != null)
            {
                await GetCountries();
            }
        }

        async Task DeleteForever(Guid id)
        {
            var response = await Service.SendRequest("Delete", $"Countries/{id}");
            if (response != null)
            {
                await GetCountries();
            }
        }
    }
}
