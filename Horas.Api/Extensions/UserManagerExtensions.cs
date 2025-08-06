using Horas.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Horas.Api.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<Domain.Person> FindUserByClaimsPrincipleWithAddress(this UserManager<Domain.Person> userManager,
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(x => x.Addresses)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<Domain.Person> FindByEmailFromClaimsPrincipal(this UserManager<Domain.Person> userManager,
            ClaimsPrincipal user)
        {
            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
}
