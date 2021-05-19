using System.Collections.Generic;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Trainer,Gym", Policy = "FilledSurveyForm")]
    public partial class ActiveMembers : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        private ICollection<MemberGymsViewModel> _memberGyms;
        private ICollection<MemberStudentsViewModel> _memberStudents;

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
                if (_role.Equals("Gym"))
                {
                    var response = await Service.SendRequest("Get", $"Details/NonActiveMemberDetailsForGym?name={user.User.Identity.Name}");
                    if (response.IsSuccessStatusCode)
                    {
                        _memberGyms = await Service.GetAll<MemberGymsViewModel>(response);
                    }
                }
                else if (_role.Equals("Trainer"))
                {
                    var response = await Service.SendRequest("Get", $"Details/NonActiveMemberDetailsForTrainer?name={user.User.Identity.Name}");
                    if (response.IsSuccessStatusCode)
                    {
                        _memberStudents = await Service.GetAll<MemberStudentsViewModel>(response);
                    }
                }
            }
        }
    }
}
