
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CHS.Web.Areas.Common.Pages
{
    //[Authorize]
    public partial class Questionnaire : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private QuestionnaireViewModel _model = new QuestionnaireViewModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("import", "/Jobie/vendor/jquery-steps/build/jquery.steps.min.js");
            await JSRuntime.InvokeVoidAsync("import", "/Jobie/vendor/jquery-validation/jquery.validate.min.js");
            await JSRuntime.InvokeVoidAsync("import", "/Jobie/js/plugins-init/jquery.validate-init.js");
            await JSRuntime.InvokeVoidAsync("import", "/Jobie/js/plugins-init/jquery-steps-init.js");
        }
    }
}
