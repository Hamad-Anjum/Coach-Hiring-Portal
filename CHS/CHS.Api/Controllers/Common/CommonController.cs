
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CHS.Api.Controllers.Common
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {

    }
}
