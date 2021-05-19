using System.Collections.Generic;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Student,Gym,Trainer", Policy = "FilledSurveyForm")]
    public partial class CompletedCourse : ComponentBase
    {
        [Inject]
        public IHttpService Serivce { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorage { get; set; }

        [Inject]
        public IRefreshService RefreshService { get; set; }

        private ICollection<CompleteCourseViewModel> _completedCourses;

        protected override async Task OnInitializedAsync()
        {
            RefreshService.CallRequestRefresh();
            var value = await LocalStorage.GetAsync<string>("id");
            if (value.Success)
            {
                var response = await Serivce.SendRequest("Get", Constant.ApiUrl + $"?id={value.Value}");
                if (response != null)
                {
                    _completedCourses = await Serivce.GetAll<CompleteCourseViewModel>(response);
                }
            }
        }
    }
}
