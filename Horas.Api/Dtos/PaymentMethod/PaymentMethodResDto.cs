namespace Horas.Api.Dtos.PaymentMethod
{
    public class PaymentMethodResDto
    {


        public Guid Id { get; set; }
        public bool IsDefault { get; set; }
        // Visa info
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpireDate { get; set; }
        public string? CVV { get; set; }
        // VodafoneCash, OrangeCash
        public string? PhoneNumber { get; set; }
        // Fawry
        public string? FawryCode { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public ICollection<OrderResDto> Orders { get; set; }
        //asmaa read only
        public PaymentMethodType PaymentType { get; set; }
        public string paymentDetails { get; set; }

    }
}
