using System.Threading.Tasks;

using MatBlazor;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CHS.Web.Shared
{
    public class CustomSlider : MatSlideToggle<bool>
    {
        [Parameter]
        public string[] Ids { get; set; }

        [Parameter]
        public int Tab { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Tab == 3)
            {
                await SkillsCheckbox();

            }
            else if (Tab == 4)
            {
                await CertificationsCheckbox();
            }
        }

        private async Task SkillsCheckbox()
        {
            await JSRuntime.InvokeVoidAsync("SkillsCheckbox", Ids);
        }

        private async Task CertificationsCheckbox()
        {
            await JSRuntime.InvokeVoidAsync("CertificationsCheckbox", Ids);
        }
    }
}
