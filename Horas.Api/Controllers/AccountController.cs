using Customer = Horas.Domain.Customer;
using Person = Horas.Domain.Person;

namespace Horas.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<Person> userManager, SignInManager<Person> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var user = await userManager.FindByEmailFromClaimsPrincipal(User);


            return new UserDto
            {
                Email = user.Email,
                Token = await tokenService.CreateToken(user),
                DisplayName = user.FirstName + " " + user.LastName,
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }


        //[Authorize]
        //[HttpGet("address")]
        //public async Task<ActionResult<List<AddressDto>>> GetUserAddress()
        //{
        //    var cust = await userManager.FindUserByClaimsPrincipleWithAddress(User);

        //    return mapper.Map<List<Address>, List<AddressDto>>(cust.Addresses);
        //}

        //[Authorize]
        //[HttpPut("address")]
        //public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        //{
        //    var cust = await userManager.FindUserByClaimsPrincipleWithAddress(HttpContext.User);

        //    cust.Address = mapper.Map<AddressDto, Address>(address);

        //    var result = await userManager.UpdateAsync(cust);

        //    if (result.Succeeded) return Ok(mapper.Map<Address, AddressDto>(cust.Address));

        //    return BadRequest("Problem updating the cust");
        //}

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized("Invalid login attempt");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid login attempt");

            return new UserDto
            {
                Email = user.Email,
                Token = await tokenService.CreateToken(user),
                DisplayName = user.FirstName
            };
        }

        [AllowAnonymous]
        [HttpPost("register/customer")]
        public async Task<ActionResult<UserDto>> Register([FromBody] CustomerRegisterDto registerDto)
        {

            // Validate
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var details = errors.Any() ? string.Join(" | ", errors) : "Validation failed";
                return BadRequest(details);
            }

            // Check if email already exists
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return BadRequest(new { message = "Email address is in use" });
            }

            // Create Customer entity
            var cust = new Customer
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await userManager.CreateAsync(cust, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorDetails = result.Errors
                    .Select(e => e.Description)
                    .ToList();
                return BadRequest(errorDetails);
            }

            // Assign Customer role
            await userManager.AddToRoleAsync(cust, "Customer");

            return new UserDto
            {
                DisplayName = cust.FirstName + " " + cust.LastName,
                Token = await tokenService.CreateToken(cust),
                Email = cust.Email,
                UserRoleH = string.Join(",", await userManager.GetRolesAsync(cust))
            };
        }

        [AllowAnonymous]
        [HttpPost("register/supplier")]
        public async Task<ActionResult<UserDto>> RegisterSupplier([FromBody] SupplierRegisterDto dto)
        {
            // Validate
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var details = errors.Any() ? string.Join(" | ", errors) : "Validation failed";
                return BadRequest(details);
            }

            // Check if email already exists
            if (CheckEmailExistsAsync(dto.Email).Result.Value)
            {
                return BadRequest(new { message = "Email address is in use" });
            }


            // Create Supplier entity
            var supp = new Supplier
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FactoryName = dto.FactoryName,
                Description = dto.Description,
                IFactoryPicPath = dto.IFactoryPicPath,
                BankAccountName = dto.BankAccountName,
                BankAccountNumber = dto.BankAccountNumber,
                IsBlocked = dto.IsBlocked,
                BlockUntil = dto.BlockUntil
            };

            var result = await userManager.CreateAsync(supp, dto.Password);

            if (!result.Succeeded)
            {
                var errorDetails = result.Errors
                    .Select(e => e.Description)
                    .ToList();
                return BadRequest(new { errors = errorDetails });
            }

            // Assign Supplier role
            await userManager.AddToRoleAsync(supp, "Supplier");

            return new UserDto
            {
                DisplayName = supp.FirstName + " " + supp.LastName,
                Token = await tokenService.CreateToken(supp),
                Email = supp.Email,
                UserRoleH = string.Join(",", await userManager.GetRolesAsync(supp))
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register/admin")]
        public async Task<ActionResult<UserDto>> RegisterAdmin([FromBody] AdminRegisterDto registerDto)
        {
            // Validate request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var details = errors.Any() ? string.Join(" | ", errors) : "Validation failed";
                return BadRequest(details);
            }

            // Check if email exists
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return BadRequest(new { message = "Email address is in use" });
            }

            // Create Person entity for admin
            var admin = new Person
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };

            // Create the admin user
            var result = await userManager.CreateAsync(admin, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorDetails = result.Errors
                    .Select(e => e.Description)
                    .ToList();
                return BadRequest(errorDetails);
            }

            // Assign Admin role
            await userManager.AddToRoleAsync(admin, "Admin");

            // Return the created admin details
            return new UserDto
            {
                DisplayName = $"{admin.FirstName} {admin.LastName}",
                Token = await tokenService.CreateToken(admin),
                Email = admin.Email,
                UserRoleH = string.Join(",", await userManager.GetRolesAsync(admin))
            };
        }

        [Authorize(Roles = "Admin")] // optional: only Admins can see Admins
        [HttpGet("admins")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAdmins()
        {
            try
            {
                var admins = await userManager.GetUsersInRoleAsync("Admin");

                if (admins == null || !admins.Any())
                    return NotFound(new { message = "No admins found." });

                var adminDtos = admins.Select(a => new UserDto
                {
                    Email = a.Email,
                    DisplayName = $"{a.FirstName} {a.LastName}",
                    UserRoleH = "Admin"
                    // Token is usually not needed here, unless you want to generate it for each admin
                }).ToList();

                return Ok(adminDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("removeAdmin")]
        public async Task<IActionResult> RemoveAdmin([FromQuery] string email)
        {
            var admin = await userManager.FindByEmailAsync(email);
            if (admin == null)
                return NotFound(new { message = "Admin not found." });

            var roles = await userManager.GetRolesAsync(admin);
            if (!roles.Contains("Admin"))
                return BadRequest(new { message = "User is not an admin." });

            var removeRoleResult = await userManager.RemoveFromRoleAsync(admin, "Admin");
            if (!removeRoleResult.Succeeded)
                return StatusCode(500, new { errors = removeRoleResult.Errors.Select(e => e.Description) });

            var deleteResult = await userManager.DeleteAsync(admin);
            if (!deleteResult.Succeeded)
                return StatusCode(500, new { errors = deleteResult.Errors.Select(e => e.Description) });

            return Ok(new { message = "Admin removed successfully." });
        }

        #region Seller_AdminControl

        //[Authorize(Roles = "Admin")]
        //[HttpGet("pending-sellers")]
        //public async Task<IActionResult> GetPendingSellers()
        //{
        //    var users = await userManager.GetUsersInRoleAsync("PendingSeller");

        //    return Ok(users.Select(u => new { u.Id, u.Email, u.FirstName, u.LastName }));
        //}


        //[Authorize(Roles = "Admin")]
        //[HttpPost("approve-seller/{userId}")]
        //public async Task<IActionResult> ApproveSeller(string userId)
        //{
        //    var cust = await userManager.FindByIdAsync(userId);
        //    if (cust == null) return NotFound();

        //    await userManager.RemoveFromRoleAsync(cust, "PendingSeller");
        //    await userManager.AddToRoleAsync(cust, "Seller");

        //    return Ok(new { message = "Seller approved successfully" });
        //}

        #endregion

    }
}
