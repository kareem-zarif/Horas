
namespace Horas.Api.Dtos.Order
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        //public decimal TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsExist { get; set; }

        public List<OrderItemReadDto> OrderItems { get; set; }

        public PaymentMethodType PaymentType { get; set; }
    }
}
