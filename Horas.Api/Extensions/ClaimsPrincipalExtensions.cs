using System.Security.Claims;

namespace Horas.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //public static Guid GetUserId(this ClaimsPrincipal user)
        //{
        //    var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);
        //    if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        //    {
        //        throw new InvalidOperationException("User ID claim is missing or invalid.");
        //    }
        //    return userId;
        //}

        // في ملف ClaimsPrincipalExtensions.cs
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return null; // 👈 تغيير هنا
            }
            return userId;
        }
    }
}
