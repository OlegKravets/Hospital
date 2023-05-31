using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiCotroller : ControllerBase
    {
        public BaseApiCotroller(HospitalDbContext dbContext)
            : base()
        {
            DbContext = dbContext;
        }

        public HospitalDbContext DbContext { get; }
    }
}
