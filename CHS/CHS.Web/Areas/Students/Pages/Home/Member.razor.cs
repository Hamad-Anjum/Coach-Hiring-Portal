using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Trainer,Gym", Policy = "FilledSurveyForm")]
    public partial class Member : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        private ICollection<MemberTrainersViewModel> _memberTrainers;
        private ICollection<MemberGymsViewModel> _memberGyms;
        private ICollection<MemberStudentsViewModel> _memberStudents;

        private string _role = string.Empty;
        private string _searchBy = string.Empty;
        private bool _selectJoinDate = false;
        private DateTime _dateFilter = default;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (user.User.Identity.IsAuthenticated)
            {
                string id = string.Empty;
                var result = await LocalStorage.GetAsync<string>("id");
                if (result.Success)
                {
                    id = result.Value;
                }
                if (user.User.IsInRole("Student"))
                {
                    _role = "Student";
                    var response = await Service.SendRequest("Get", $"Details/TrainerDetailsForStudent?name={user.User.Identity.Name}");
                    if (response.IsSuccessStatusCode)
                    {
                        _memberTrainers = await Service.GetAll<MemberTrainersViewModel>(response);
                    }
                }
                else if (user.User.IsInRole("Gym"))
                {
                    _role = "Gym";
                    var response = await Service.SendRequest("Get", $"Details/MemberDetailsForGym?name={user.User.Identity.Name}");
                    if (response.IsSuccessStatusCode)
                    {
                        _memberGyms = await Service.GetAll<MemberGymsViewModel>(response);
                    }
                }
                else if (user.User.IsInRole("Trainer"))
                {
                    _role = "Trainer";
                    var response = await Service.SendRequest("Get", $"Details/MemberDetailsForTrainer?id={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        _memberStudents = await Service.GetAll<MemberStudentsViewModel>(response);
                    }
                }
            }
        }

        protected void DateChanged(DateTime e)
        {
            if (e == default)
            {
                _dateFilter = default;
            }
            else
            {

            }
        }

        protected void OrderByChange(string e)
        {
            if (string.IsNullOrEmpty(e))
            {
                _searchBy = "Select Filter";
            }
            else
            {
                _searchBy = e;
                if (e.Equals("FirstName"))
                {
                    if (_role.Equals("Trainer"))
                    {
                        _memberStudents = _memberStudents.OrderBy(x => x.FirstName).ToList();
                    }
                    else if (_role.Equals("Student"))
                    {
                        _memberTrainers = _memberTrainers.OrderBy(x => x.FirstName).ToList();
                    }
                    else if (_role.Equals("Gym"))
                    {
                        _memberGyms = _memberGyms.OrderBy(x => x.FirstName).ToList();
                    }
                    _dateFilter = default;
                    _selectJoinDate = false;
                }
                else if (e.Equals("LastName"))
                {
                    if (_role.Equals("Trainer"))
                    {
                        _memberStudents = _memberStudents.OrderBy(x => x.LastName).ToList();
                    }
                    else if (_role.Equals("Student"))
                    {
                        _memberTrainers = _memberTrainers.OrderBy(x => x.LastName).ToList();
                    }
                    else if (_role.Equals("Gym"))
                    {
                        _memberGyms = _memberGyms.OrderBy(x => x.LastName).ToList();
                    }
                    _dateFilter = default;
                    _selectJoinDate = false;
                }
                else if (e.Equals("JoinDate"))
                {
                    if (_role.Equals("Trainer"))
                    {
                        _memberStudents = _memberStudents.OrderBy(x => x.JoinDate).ToList();
                    }
                    else if (_role.Equals("Student"))
                    {
                        _memberTrainers = _memberTrainers.OrderBy(x => x.SubscriptionDate).ToList();
                    }
                    else if (_role.Equals("Gym"))
                    {
                        _memberGyms = _memberGyms.OrderBy(x => x.SubscriptionDate).ToList();
                    }
                    _dateFilter = default;
                    _selectJoinDate = false;
                }
                else if (e.Equals("Designation"))
                {
                    if (_role.Equals("Trainer"))
                    {
                        _memberStudents = _memberStudents.OrderBy(x => x.Designation).ToList();
                    }
                    else if (_role.Equals("Student"))
                    {
                        _memberTrainers = _memberTrainers.OrderBy(x => x.Designation).ToList();
                    }
                    else if (_role.Equals("Gym"))
                    {
                        _memberGyms = _memberGyms.OrderBy(x => x.Designation).ToList();
                    }
                    _dateFilter = default;
                    _selectJoinDate = false;
                }
                else if (e.Equals("Active"))
                {
                    if (_role.Equals("Trainer"))
                    {
                        _memberStudents = _memberStudents.OrderByDescending(x => x.IsStillActive).ToList();
                    }
                    else if (_role.Equals("Gym"))
                    {
                        _memberGyms = _memberGyms.OrderByDescending(x => x.IsStillActive).ToList();
                    }
                    _dateFilter = default;
                    _selectJoinDate = false;
                }
                else if (e.Equals("Date"))
                {
                    _selectJoinDate = true;
                }
            }
        }
    }
}
