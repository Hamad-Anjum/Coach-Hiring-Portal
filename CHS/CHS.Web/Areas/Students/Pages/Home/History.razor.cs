
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Gym,Trainer", Policy = "FilledSurveyForm")]
    public partial class History : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private ICollection<TrainerHistoryViewModel> _trainerHistory;
        private ICollection<GymHistoryViewModel> _gymHistory;

        private IEnumerable<StudentHistoryViewModel> _studentHistory;
        private string _role = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (user.User.Identity.IsAuthenticated)
            {
                var role = await LocalStorage.GetAsync<string>("roles");
                if (role.Success)
                {
                    _role = role.Value;
                }
                if (_role.Equals("Student"))
                {
                    var response = await Service.SendRequest("Get", $"History/GetStudentTransactionHistory?name={user.User.Identity.Name}");
                    if (response != null)
                    {
                        _studentHistory = await Service.GetAll<StudentHistoryViewModel>(response);
                    }
                }
                else if (_role.Equals("Gym"))
                {
                    var response = await Service.SendRequest("Get", $"History/GetGymTransactionHistory?name={user.User.Identity.Name}");
                    if (response != null)
                    {
                        _gymHistory = await Service.GetAll<GymHistoryViewModel>(response);
                        _gymHistory = _gymHistory.OrderBy(x => x.InstallmentNumber).ToList();
                    }
                }
                else if (_role.Equals("Trainer"))
                {
                    var response = await Service.SendRequest("Get", $"History/GetTrainerTransactionHistory?name={user.User.Identity.Name}");
                    if (response != null)
                    {
                        _trainerHistory = await Service.GetAll<TrainerHistoryViewModel>(response);
                        _trainerHistory = _trainerHistory.OrderBy(x => x.InstallmentNumber).ToList();
                    }
                }
            }
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    await JSRuntime.InvokeVoidAsync("window.Custom2");
        //    await JSRuntime.InvokeVoidAsync("window.Custom3");
        //    await JSRuntime.InvokeVoidAsync("window.DevNav");
        //    //await JSRuntime.InvokeVoidAsync("window.carousel");
        //    await JSRuntime.InvokeVoidAsync("window.LoadChart");
        //    await JSRuntime.InvokeVoidAsync("window.Dashboard1");
        //    await JSRuntime.InvokeVoidAsync("carouselReview");
        //    //await JSRuntime.InvokeVoidAsync("import", "/Jobie/vendor/chartist-plugin-tooltips/js/chartist-plugin-tooltip.min.js");
        //    //await JSRuntime.InvokeVoidAsync("import", "/JavaScript.js");
        //}
    }
}
