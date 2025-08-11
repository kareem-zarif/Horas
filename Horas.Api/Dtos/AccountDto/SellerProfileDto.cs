namespace Horas.Api.Dtos.AccountDto
{
    public class SellerProfileDto
    {
        [Required(ErrorMessage = "Store name is required")]
        [StringLength(100, ErrorMessage = "Store name cannot exceed 100 characters")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Business type is required")]
        [StringLength(50, ErrorMessage = "Business type cannot exceed 50 characters")]
        public string BusinessType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(\+2|002|02|01)[0-9]{8,11}$", ErrorMessage = "Please enter a valid Egyptian phone number (01XXXXXXXX, +201XXXXXXXX, or 002XXXXXXXX)")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? WebsiteUrl { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the logo")]
        public string? StoreLogoUrl { get; set; }   
    }
}
