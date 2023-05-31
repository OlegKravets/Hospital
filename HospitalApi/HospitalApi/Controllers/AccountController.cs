using HospitalApi.DTOs;
using HospitalApi.Interfaces;
using HospitalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HospitalApi.Controllers
{
    [Authorize]
    public class AccountController : BaseApiCotroller
    {
        private readonly ITokenService _tokenService;

        public AccountController(HospitalDbContext dbContext, ITokenService tokenService)
            : base(dbContext)
        {
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

            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            return new UserDto()
            {
                UserName = newUser.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto loginModel)
        {
            var user = await DbContext.Users.SingleOrDefaultAsync(
                u => u.Name == loginModel.UserName);

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

            return user;
        }

        [AllowAnonymous]
        [HttpPost("delete")]
        public async Task<ActionResult<bool>> Delete(string userName)
        {
            var user = await DbContext.Users.SingleOrDefaultAsync(u => u.Name == userName);

            if (user is null)
            {
                return Unauthorized("Invalid Username");
            }

            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> UserExist(string userName)
        {
            return await DbContext.Users.AnyAsync(u => u.Name.ToUpper() == userName.ToUpper());
        }
    }
}
