

namespace Horas.Api.Dtos.Customer
{
    public class CustomerResDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }

        public int OrdersCount { get; set; }
        public  List<OrderReadDto> Orders { get; set; }
        public List<PaymentMethodResDto> PaymentMethods { get; set; }

        public  List<MessageResDto> Messages { get; set; }

        public List<ReviewResDto> Reviews { get; set; }
        public WishlistResDto Wishlist { get; set; }

        public CartResDto Cart { get; set; }

        public List<NotificationReadDto> Notifications { get; set; }
        public List<ReportResDto> Reports { get; set; }

        // public PaymentMethodType DefaultPaymentMethodType { get; set; } = PaymentMethodType.Cash;


        //public int MessagesCount { get; set; }



    }
}
