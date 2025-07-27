
namespace Horas.Api.Dtos.PaymentMethod
{
    public class PaymentMethodResDto
    {
        Guid Guid { get; set; }
        public PaymentMethodType PaymentType { get; set; }
        //public string PhoneNumber { get; set; }

        //public string CardNumber { get; set; }
        //public string CardHolderName { get; set; }
        public string paymentDetails { get; set; }  
    }
}
