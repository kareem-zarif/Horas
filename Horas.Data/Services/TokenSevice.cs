// تحديث TokenService لإضافة معلومات إضافية في الـ JWT
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Horas.Data.Services
{
    public class TokenSevice : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Person> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenSevice(IConfiguration config, UserManager<Person> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]!));
        }

        public async Task<string> CreateToken(Person user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? ""),
                new Claim(ClaimTypes.Surname, user.LastName ?? "")
            };

            // إضافة الـ roles
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // إضافة معلومة عن اكتمال الـ profile للبائع
            if (roles.Contains("Seller"))
            {
                //claims.Add(new Claim("IsSellerProfileComplete", user.IsSellerProfileComplete.ToString()));

                //// إضافة معلومات إضافية إذا كان الـ profile مكتمل
                //if (user.SellerProfile != null)
                //{
                //    claims.Add(new Claim("StoreName", user.SellerProfile.StoreName ?? ""));
                //}
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}