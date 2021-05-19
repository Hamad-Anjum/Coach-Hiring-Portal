
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace CHS.Web.Areas.Students.Pages.Home
{
    [Authorize(Roles = "Gym,Trainer", Policy = "FilledSurveyForm")]
    public partial class Installment : ComponentBase
    {

    }
}
