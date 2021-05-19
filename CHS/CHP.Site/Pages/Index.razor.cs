using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CHP.Site.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private SiteViewModel _model = new();

        private bool _isFullText;

        protected override async Task OnInitializedAsync()
        {
            var response = await Service.SendRequest("Get", "Site", true);

            if (response.IsSuccessStatusCode)
            {
                _model = await Service.Get<SiteViewModel>(response);
            }
            else
            {
                _model.GymMemberShips = new List<GymMemberShipViewModel>();
                _model.BestGyms = new List<BestGymViewModel>();
                _model.TrainerPrograms = new List<TrainerProgramViewModel>();
                _model.BestTrainer = new List<BestTrainerViewModel>();
                _model.LatestGyms = new List<LatestGymViewModel>();
                _model.LatestTrainers = new List<LatestTrainerViewModel>();
            }
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (!firstRender)
        //    {
        //        await JSRuntime.InvokeVoidAsync("window.BestTrainer");
        //    }
        //}

        private void ToggleMemberShipFullText(Guid id)
        {
            var subscription = _model.GymMemberShips.Where(x => x.SubscriptionId == id).FirstOrDefault();
            subscription.IsFullText = !subscription.IsFullText;
            _model.GymMemberShips.Remove(subscription);
            _model.GymMemberShips.Add(subscription);
            _isFullText = !_isFullText;
        }

        private void ToggleLatestGymFullText(Guid id)
        {
            var subscription = _model.LatestGyms.Where(x => x.Id == id).FirstOrDefault();
            subscription.IsFullText = !subscription.IsFullText;
            _model.LatestGyms.Remove(subscription);
            _model.LatestGyms.Add(subscription);
            _isFullText = !_isFullText;
        }

        private void ToggleLatestTrainerFullText(Guid id)
        {
            var subscription = _model.LatestTrainers.Where(x => x.Id == id).FirstOrDefault();
            subscription.IsFullText = !subscription.IsFullText;
            _model.LatestTrainers.Remove(subscription);
            _model.LatestTrainers.Add(subscription);
            _isFullText = !_isFullText;
        }
    }
}
