using Horas.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horas.Api.Dtos.PaymentMethod
{
    public class PaymentMethodCreateDto
    {
        [Required]
        public PaymentMethodType PaymentType { get; set; }

        public bool IsDefault { get; set; } = false;

        // Visa
        public string? CardNumber { get; set; }

        public string? CardHolderName { get; set; }

        public string? ExpireDate { get; set; }

        public string? CVV { get; set; }

        // Vodafone / Orange Cash
        [Phone, MinLength(11), MaxLength(13)]
        [RegularExpression(@"^(010|011|012|015)\d{8,10}$", ErrorMessage = "PhoneNumber number must start with 010, 011, 012, or 015")]
        public string? PhoneNumber { get; set; }

        // Fawry
        public string? FawryCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}
