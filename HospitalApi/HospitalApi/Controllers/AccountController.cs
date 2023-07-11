using HospitalApi.DTOs;
using HospitalApi.Interfaces;
using HospitalApi.Models;
using HospitalApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace HospitalApi.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly UserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AccountController(UserRepository repository, ITokenService tokenService)
            : base()
        {
            _userRepository = repository;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto newUser)
        {
            if (await UserExist(newUser.UserName))
            {
                return BadRequest("Username is taken!");
            }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Name = newUser.UserName,
                Age = newUser.Age,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newUser.Password)),
                PasswordSalt = hmac.Key,
            };

            await _userRepository.AddUser(user);

            return new UserDto()
            {
                Username = newUser.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginModel)
        {
            User user = await _userRepository.GetUserByUsername(loginModel.UserName);

            if (user is null)
            {
                return Unauthorized("Invalid Username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginModel.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }

            return new UserDto()
            {
                Username = user.Name,
                Token = _tokenService.CreateToken(user)
            };
        }

        [AllowAnonymous]
        [HttpPost("delete")]
        public async Task<ActionResult<bool>> Delete(string userName)
        {
            User user = await _userRepository.GetUserByUsername(userName);

            if (user is null)
            {
                return Unauthorized("Invalid Username");
            }

            await _userRepository.RemoveUser(user.Id);

            return true;
        }

        private async Task<bool> UserExist(string userName)
        {
            return await _userRepository.IsUserExist(userName);
        }
    }
}
