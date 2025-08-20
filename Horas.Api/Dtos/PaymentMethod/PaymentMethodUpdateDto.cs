using Horas.Domain;
using System.ComponentModel.DataAnnotations;

namespace Horas.Api.Dtos.PaymentMethod
{
    public class PaymentMethodUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

        public PaymentMethodType PaymentType { get; set; }

        public bool IsDefault { get; set; }

        public string? CardNumber { get; set; }

        public string? CardHolderName { get; set; }

        public string? ExpireDate { get; set; }

        public string? CVV { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FawryCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}
