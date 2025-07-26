using System.ComponentModel.DataAnnotations;

namespace Horas.Api.Dtos.AccountDto
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z]).{8,32}$",
            ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number and at least 8 characters")]
        public string Password { get; set; }


        public string RequestedRole { get; set; } = "Customer";
    }
}
