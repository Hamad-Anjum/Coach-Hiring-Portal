
using System.Threading.Tasks;

using CHS.Services.IService;

using Microsoft.AspNetCore.Components;

namespace CHP.Site.Pages
{
    public partial class SearchTrainer : ComponentBase
    {
        [Inject]
        public IHttpService Service { get; set; }


        protected override async Task OnInitializedAsync()
        {

        }
    }
}
