using HospitalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiCotroller
    {
        public UsersController(HospitalDbContext dbContext)
            : base(dbContext)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = DbContext.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}
