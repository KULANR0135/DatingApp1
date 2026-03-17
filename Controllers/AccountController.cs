using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;
using DatingApp.DTOs;
using Microsoft.EntityFrameworkCore;
using DatingApp.Services;
using DatingApp.Extensions;
using DatingApp.Interfaces;
namespace DatingApp.Controllers
{
   
    public class AccountController(AppDbContext context, ITokenService tokenservice) : BaseApiController
    {
        [HttpPost("register")]  //api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerdto)
        {
             if(await EmailExists(registerdto.Email)) return BadRequest("email taken");
            using var hmac = new HMACSHA512();

            var user = new AppUser()
            {
                DisplayName = registerdto.DisplayName,
                Email = registerdto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerdto.Password)),
                PasswordSalt = hmac.Key
            };


            context.Users.Add(user);
            await context.SaveChangesAsync();

            //replacing with extension method
            return user.ToDto(tokenservice);


        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logindto)
        {
            var user= await context.Users.SingleOrDefaultAsync(x=>x.Email == logindto.Email);
            if (user == null) return Unauthorized("invalid email address");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));
            for(var i=0;i<computedhash.Length;i++)
            {
                if (computedhash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            }

            //replacing with extension methods
            return user.ToDto(tokenservice);

        }

        //to avoid duplicate email address exists
        private async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
