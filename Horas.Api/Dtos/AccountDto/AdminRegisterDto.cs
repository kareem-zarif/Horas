namespace Horas.Api.Dtos.AccountDto
{
    public class AdminRegisterDto
    {
        [Required, MinLength(3), MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string LastName { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress), MaxLength(50)]
        public string Email { get; set; }
        [Required, RegularExpression("^(?=.*[0-9])(?=.*[a-zA-Z]).{10,32}$",
            ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number and at least 8 characters")]
        public string Password { get; set; }
        [Required, RegularExpression(@"^(\+2|002|02|01)[0-9]{8,11}$", ErrorMessage = "Please enter a valid Egyptian phone number (01XXXXXXXX, +201XXXXXXXX, or 002XXXXXXXX)")]
        public string PhoneNumber { get; set; }
    }
}
