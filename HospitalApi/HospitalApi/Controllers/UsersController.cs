using HospitalApi.Models;
using HospitalApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly UserRepository _userRepository;

        public UsersController(UserRepository repository)
            : base()
        {
            _userRepository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await _userRepository.AddUser(user);
            return Ok();
        }
    }
}
