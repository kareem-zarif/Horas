using Person = Horas.Domain.Person;

namespace Horas.Api.Controllers
{
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

        [Authorize]
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
        //    var user = await userManager.FindUserByClaimsPrincipleWithAddress(User);

        //    return mapper.Map<List<Address>, List<AddressDto>>(user.Addresses);
        //}

        //[Authorize]
        //[HttpPut("address")]
        //public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        //{
        //    var user = await userManager.FindUserByClaimsPrincipleWithAddress(HttpContext.User);

        //    user.Address = mapper.Map<AddressDto, Address>(address);

        //    var result = await userManager.UpdateAsync(user);

        //    if (result.Succeeded) return Ok(mapper.Map<Address, AddressDto>(user.Address));

        //    return BadRequest("Problem updating the user");
        //}


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = await tokenService.CreateToken(user),
                DisplayName = user.FirstName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                { Errors = new[] { "Email address is in use" } });
            }

            var user = new Person
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            string roleToAssign = registerDto.RequestedRole == "Seller" ? "PendingSeller" : "Customer";
            await userManager.AddToRoleAsync(user, roleToAssign);

            return new UserDto
            {
                DisplayName = user.FirstName + " " + user.LastName,
                Token = await tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        #region Seller_AdminControl

        [Authorize(Roles = "Admin")]
        [HttpGet("pending-sellers")]
        public async Task<IActionResult> GetPendingSellers()
        {
            var users = await userManager.GetUsersInRoleAsync("PendingSeller");

            return Ok(users.Select(u => new { u.Id, u.Email, u.FirstName, u.LastName }));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("approve-seller/{userId}")]
        public async Task<IActionResult> ApproveSeller(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await userManager.RemoveFromRoleAsync(user, "PendingSeller");
            await userManager.AddToRoleAsync(user, "Seller");

            return Ok(new { message = "Seller approved successfully" });
        }

        #endregion

    }
}
