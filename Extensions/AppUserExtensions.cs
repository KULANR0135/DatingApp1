using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;

namespace DatingApp.Extensions
{
    public static class AppUserExtensions
    {
        public static UserDto ToDto(this AppUser user, ITokenService tokenservice)
        {
            return new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = tokenservice.CreateToken(user)
            };
        }
    }
}
