// تعديل الـ Custom Filter لاستخدامه فقط في الصفحات التي تتطلب profile مكتمل
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Horas.Api.Extensions;
using System.Security.Claims;

namespace Horas.Api.CustomFilter
{
    public class RequireCompleteSellerProfileAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userManager = context.HttpContext.RequestServices.GetService<UserManager<Domain.Person>>();
            var userClaims = context.HttpContext.User;

            var userId = userClaims.GetUserId();

            if (userId == null)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Unauthorized." });
                return;
            }

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "User not found." });
                return;
            }

            var isSeller = await userManager.IsInRoleAsync(user, "Seller");

            if (!isSeller)
            {
                context.Result = new ForbidResult("You are not authorized as a seller.");
                return;
            }

            // هنا الجزء المهم: فقط نتحقق من اكتمال الـ profile في الصفحات المحددة
            // لا نطبق هذا الشرط على endpoint إكمال الـ profile نفسه
            if (!user.IsSellerProfileComplete)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Please complete your seller profile first.",
                    redirectTo = "/seller-profile"
                });
                return;
            }

            await next();
        }
    }

    // إنشاء attribute منفصل للتحقق من الـ Seller Role فقط بدون التحقق من اكتمال الـ Profile
    public class RequireSellerRoleAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userManager = context.HttpContext.RequestServices.GetService<UserManager<Domain.Person>>();
            var userClaims = context.HttpContext.User;

            var userId = userClaims.GetUserId();

            if (userId == null)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Unauthorized." });
                return;
            }

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "User not found." });
                return;
            }

            var isSeller = await userManager.IsInRoleAsync(user, "Seller");

            if (!isSeller)
            {
                context.Result = new ForbidResult("You are not authorized as a seller.");
                return;
            }

            await next();
        }
    }
}