using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
            : base()
        {
        }
    }
}
