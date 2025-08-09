
// الحل الأول: تعديل ProfileSellerController للسماح للبائعين الغير مكتملي الـ profile
namespace Horas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileSellerController : ControllerBase
    {
        private readonly HorasDBContext _context;

        public ProfileSellerController(HorasDBContext context)
        {
            _context = context;
        }

        [HttpPost("complete")]
        [Authorize(Roles = "Seller")] // فقط التحقق من الـ Role, بدون التحقق من اكتمال الـ Profile
        public async Task<IActionResult> CompleteSellerProfile([FromBody] SellerProfileDto dto)
        {
            var userId = User.GetUserId();
            var person = await _context.Users
                .Include(u => u.SellerProfile) // إضافة Include للتأكد من جلب الـ Profile
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (person == null)
                return NotFound("User not found.");

            // التحقق من أن المستخدم هو بائع
            if (!User.IsInRole("Seller"))
                return Forbid("You are not authorized as a seller.");

            // التحقق من عدم اكتمال الـ Profile مسبقاً
            if (person.SellerProfile != null && person.IsSellerProfileComplete)
                return BadRequest(new { message = "Profile already completed." });

            // إنشاء أو تحديث الـ Profile
            SellerProfile profile;

            if (person.SellerProfile == null)
            {
                // إنشاء profile جديد
                profile = new SellerProfile
                {
                    Id = Guid.NewGuid(),
                    PersonId = person.Id,
                    StoreName = dto.StoreName,
                    BusinessType = dto.BusinessType,
                    Description = dto.Description,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    WebsiteUrl = dto.WebsiteUrl,
                    StoreLogoUrl = dto.StoreLogoUrl
                };

                person.SellerProfile = profile;
                _context.SellerProfiles.Add(profile);
            }
            else
            {
                // تحديث الـ profile الموجود
                person.SellerProfile.StoreName = dto.StoreName;
                person.SellerProfile.BusinessType = dto.BusinessType;
                person.SellerProfile.Description = dto.Description;
                person.SellerProfile.PhoneNumber = dto.PhoneNumber;
                person.SellerProfile.Address = dto.Address;
                person.SellerProfile.WebsiteUrl = dto.WebsiteUrl;
                person.SellerProfile.StoreLogoUrl = dto.StoreLogoUrl;
            }

            // تحديد أن الـ Profile مكتمل
            person.IsSellerProfileComplete = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Seller profile completed successfully." });
        }

        // إضافة endpoint للتحقق من حالة الـ profile
        //[HttpGet("status")]
        //[Authorize(Roles = "Seller")]
        //public async Task<IActionResult> GetProfileStatus()
        //{
        //    var userId = User.GetUserId();
        //    var person = await _context.Users
        //        .Include(u => u.SellerProfile)
        //        .FirstOrDefaultAsync(p => p.Id == userId);

        //    if (person == null)
        //        return NotFound("User not found.");

        //    return Ok(new
        //    {
        //        isComplete = person.IsSellerProfileComplete,
        //        hasProfile = person.SellerProfile != null,
        //        profileData = person.SellerProfile
        //    });
        //}

        [HttpGet("status")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetProfileStatus()
        {
            try
            {
                var userId = User.GetUserId();
                if (!userId.HasValue)
                {
                    return Unauthorized("User ID claim is missing or invalid.");
                }

                var person = await _context.Users
                    .Include(u => u.SellerProfile)
                    .FirstOrDefaultAsync(p => p.Id == userId.Value);

                if (person == null)
                    return NotFound("User not found.");

                // استخدم DTO بدل الكائن المباشر
                var profileData = person.SellerProfile != null
                    ? new SellerProfileDto
                    {
                        StoreName = person.SellerProfile.StoreName,
                        BusinessType = person.SellerProfile.BusinessType,
                        Description = person.SellerProfile.Description,
                        PhoneNumber = person.SellerProfile.PhoneNumber,
                        Address = person.SellerProfile.Address,
                        WebsiteUrl = person.SellerProfile.WebsiteUrl,
                        StoreLogoUrl = person.SellerProfile.StoreLogoUrl
                    }
                    : null;

                return Ok(new
                {
                    isComplete = person.IsSellerProfileComplete,
                    hasProfile = person.SellerProfile != null,
                    profileData = profileData
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetProfileStatus: {ex.Message}\nStackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return StatusCode(500, new
                {
                    message = "An internal server error occurred.",
                    details = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }

        //new way

        [HttpGet("profile")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetSellerProfile()
        {
            var userId = User.GetUserId();
            if (!userId.HasValue)
                return Unauthorized("User ID claim is missing or invalid.");

            var person = await _context.Users
                .Include(u => u.SellerProfile)
                .FirstOrDefaultAsync(p => p.Id == userId.Value);

            if (person == null)
                return NotFound("User not found.");

            if (person.SellerProfile == null)
                return NotFound("Seller profile not found.");

            var dto = new SellerProfileDto
            {
                StoreName = person.SellerProfile.StoreName,
                BusinessType = person.SellerProfile.BusinessType,
                Description = person.SellerProfile.Description,
                PhoneNumber = person.SellerProfile.PhoneNumber,
                Address = person.SellerProfile.Address,
                WebsiteUrl = person.SellerProfile.WebsiteUrl,
                StoreLogoUrl = person.SellerProfile.StoreLogoUrl
            };

            return Ok(dto);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> UpdateSellerProfile([FromBody] SellerProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            if (!userId.HasValue)
                return Unauthorized("User ID claim is missing or invalid.");

            var person = await _context.Users
                .Include(u => u.SellerProfile)
                .FirstOrDefaultAsync(p => p.Id == userId.Value);

            if (person == null)
                return NotFound("User not found.");

            if (person.SellerProfile == null)
                return NotFound("Seller profile not found.");

            // تحديث البيانات
            person.SellerProfile.StoreName = dto.StoreName;
            person.SellerProfile.BusinessType = dto.BusinessType;
            person.SellerProfile.Description = dto.Description;
            person.SellerProfile.PhoneNumber = dto.PhoneNumber;
            person.SellerProfile.Address = dto.Address;
            person.SellerProfile.WebsiteUrl = dto.WebsiteUrl;
            person.SellerProfile.StoreLogoUrl = dto.StoreLogoUrl;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Seller profile updated successfully." });
        }


    }
}



//if (!user.IsSellerProfileComplete)
//return Forbid("Please complete your seller profile.");
