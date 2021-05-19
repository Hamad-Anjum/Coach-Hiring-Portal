using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Gym,Trainer", Policy = "FilledSurveyForm")]
    public partial class SearchGym : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        private ICollection<SearchViewModel> _searchViewModels;

        private string _filter = string.Empty, _searchTerm, _searchByText = "Title", _searchBy = "Title", _placeHolder = "Search By Title";
        private bool _showFilter, _byPriceFilter = false;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();

            var response = await Service.SendRequest("Get", $"SearchGym");
            if (response.IsSuccessStatusCode)
            {
                _searchViewModels = await Service.GetAll<SearchViewModel>(response);
            }
            else
            {
                _searchViewModels = new List<SearchViewModel>();
            }
        }

        private void SearchBy(string e)
        {
            _searchBy = e;
            if (!string.IsNullOrEmpty(e))
            {
                _placeHolder = "Search By " + e;
            }
            else
            {
                _placeHolder = "Search By Title...";
            }
            _searchByText = e;
        }

        private async Task ChangeFilter(string e)
        {
            await JSRuntime.InvokeVoidAsync("AddBackground", e.Split(' ')[0]);
            if (_filter.Equals(e))
            {
                _filter = string.Empty;
                var response = await Service.SendRequest("Get", $"SearchGym");
                if (response.IsSuccessStatusCode)
                {
                    _searchViewModels = await Service.GetAll<SearchViewModel>(response);
                }
                else
                {
                    _searchViewModels = new List<SearchViewModel>();
                }
            }
            else
            {
                _filter = e;
                var response = await Service.SendRequest("Get", $"SearchGym/GetByDesignation?searchTerm={_filter}");
                if (response.IsSuccessStatusCode)
                {
                    _searchViewModels = await Service.GetAll<SearchViewModel>(response);
                }
                else
                {
                    _searchViewModels = new List<SearchViewModel>();
                }
            }
        }

        private async Task ChangeSearchTerm(string e)
        {
            _searchTerm = e;
            if (!string.IsNullOrEmpty(_searchBy) && !string.IsNullOrEmpty(e))
            {
                _searchViewModels = null;
                if (_searchBy.Contains("Name"))
                {
                    var response = await Service.SendRequest("Get", $"SearchGym/GetByName?searchTerm={e}");
                    if (response.IsSuccessStatusCode)
                    {
                        _searchViewModels = await Service.GetAll<SearchViewModel>(response);
                    }
                    else
                    {
                        _searchViewModels = new List<SearchViewModel>();
                    }
                }
                if (_searchBy.Contains("Title"))
                {
                    var response = await Service.SendRequest("Get", $"SearchGym/GetByDesignation?searchTerm={e}");
                    if (response.IsSuccessStatusCode)
                    {
                        _searchViewModels = await Service.GetAll<SearchViewModel>(response);
                    }
                    else
                    {
                        _searchViewModels = new List<SearchViewModel>();
                    }
                }
                if (_searchBy.Contains("District"))
                {
                    var response = await Service.SendRequest("Get", $"SearchGym/GetByDistrict?searchTerm={e}");
                    if (response.IsSuccessStatusCode)
                    {
                        _searchViewModels = await Service.GetAll<SearchViewModel>(response);
                    }
                    else
                    {
                        _searchViewModels = new List<SearchViewModel>();
                    }
                }
                else if (_searchBy.Contains("City"))
                {
                    var response = await Service.SendRequest("Get", $"SearchGym/GetByCity?searchTerm={e}");
                    if (response.IsSuccessStatusCode)
                    {
                        _searchViewModels = await Service.GetAll<SearchViewModel>(response);
                    }
                    else
                    {
                        _searchViewModels = new List<SearchViewModel>();
                    }
                }
            }
            else
            {
                var response = await Service.SendRequest("Get", $"SearchGym");
                if (response.IsSuccessStatusCode)
                {
                    _searchViewModels = await Service.GetAll<SearchViewModel>(response);
                }
                else
                {
                    _searchViewModels = new List<SearchViewModel>();
                }
            }
        }

        private async Task Find()
        {
            await ChangeSearchTerm(_searchTerm);
        }

        private async Task SpaceAvailableFilter(bool searchTerm)
        {
            var response = await Service.SendRequest("Get", $"SearchGym/GetBySpaceAvailable?searchTerm={searchTerm}");
            if (response.IsSuccessStatusCode)
            {
                _searchViewModels = await Service.GetAll<SearchViewModel>(response);
            }
            else
            {
                _searchViewModels = new List<SearchViewModel>();
            }
        }

        private void ByPriceFilter()
        {
            _byPriceFilter = !_byPriceFilter;
        }

        private void ChageOrderBy(string e)
        {
            if (e.Equals("Newest"))
            {
                _searchViewModels = _searchViewModels.OrderBy(x => x.CreatedAt).ToList();
            }
            else if (e.Equals("Low"))
            {
                _searchViewModels = _searchViewModels.OrderBy(x => x.MinPrice).ToList();
            }
            else if (e.Equals("High"))
            {
                _searchViewModels = _searchViewModels.OrderByDescending(x => x.MinPrice).ToList();
            }
            else if (string.IsNullOrEmpty(e))
            {
                _searchViewModels = _searchViewModels.OrderByDescending(x => x.CreatedAt).ToList();
            }
        }

        private void ShowFilter()
        {
            _showFilter = !_showFilter;
        }
    }
}
