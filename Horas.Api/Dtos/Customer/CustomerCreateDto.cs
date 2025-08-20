

namespace Horas.Api.Dtos.Customer
{
    public class CustomerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required, MinLength(11), MaxLength(13), Phone, RegularExpression(@"^(010|011|012|15)\d{8,10}$", ErrorMessage = "PhoneNumber number must start with 010, 011,015, or 012 and be between 11 and 13 digits")]
        public string PhoneNumber { get; set; }
    }
}
