
using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        public string Hello()
        {
            return "Working";
        }
    }
}
