using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Services.Aad;

namespace Horas.Data.DataAccess;

public class StoreContextSeed
{
    public static async Task SeedAsync(HorasDBContext context, UserManager<Person> userManager , RoleManager<Role> roleManager)
    {
        if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
        {
            string[] roles = { "Admin", "Seller", "Customer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role });
                }
            }
            var user = new Person
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };

            await userManager.CreateAsync(user, "P@ssw0rd");
            await userManager.AddToRoleAsync(user, "Admin");


            //  for pending sellerRole
            await roleManager.CreateAsync(new Role { Name = "PendingSeller" });

        }
    }
}
