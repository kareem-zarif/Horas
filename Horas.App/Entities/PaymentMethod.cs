namespace Horas.Domain
{
    public class PaymentMethod : IBaseEnt
    {
        //if orderitem is sample will allow to him cash m if order not allow cash
        public PaymentMethodType PaymentType { get; set; } = PaymentMethodType.Stripe;
        public bool IsDefault { get; set; } = false;
        //INstapay

        //Visa
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpireDate { get; set; }
        public string? CVV { get; set; }
        //vodafoneCash , OrangeCash
        [Required, Phone, MinLength(11), MaxLength(13), RegularExpression(@"^(010|011|012|15)\d{8,10}$", ErrorMessage = "Phone number must start with 010, 011,015, or 012 and be between 11 and 13 digits")]
        public string PhoneNumber { get; set; }
        //fawry
        public string? FawryCode { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
