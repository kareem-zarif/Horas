using Microsoft.AspNetCore.Identity;

namespace Horas.Data.DataAccess;

public class StoreContextSeed
{
    public static async Task SeedAsync(HorasDBContext context, UserManager<Person> userManager, RoleManager<Role> roleManager)
    {
        // تم تبسيط الأدوار - إزالة PendingSeller
        string[] roles = { "Admin", "Supplier", "Customer" };

        // إنشاء الأدوار إن لم تكن موجودة
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Role { Name = role });
            }
        }

        // البحث عن المستخدم admin@test.com
        var user = await userManager.FindByEmailAsync("admin@test.com");
        if (user == null)
        {
            user = new Person
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
                FirstName = "Admin",
                LastName = "User"
            };
            // إنشاء المستخدم
            await userManager.CreateAsync(user, "P@ssw0rd");
        }

        // التأكد من وجود المستخدم في دور Admin
        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }

        // حذف أي مستخدمين في دور PendingSeller إذا كان موجوداً
        if (await roleManager.RoleExistsAsync("PendingSeller"))
        {
            var pendingUsers = await userManager.GetUsersInRoleAsync("PendingSeller");
            foreach (var pendingUser in pendingUsers)
            {
                await userManager.RemoveFromRoleAsync(pendingUser, "PendingSeller");
                await userManager.AddToRoleAsync(pendingUser, "Seller");
            }
            // حذف دور PendingSeller
            var pendingRole = await roleManager.FindByNameAsync("PendingSeller");
            if (pendingRole != null)
            {
                await roleManager.DeleteAsync(pendingRole);
            }
        }
    }
}