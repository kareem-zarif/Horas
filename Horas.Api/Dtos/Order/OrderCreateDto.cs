
namespace Horas.Api.Dtos.Order
{
    public class OrderCreateDto
    {
        [Required]
        public double TotalAmount { get; set; }
        public Guid? PaymentMethodId { get; set; }
        //public int? PaymentMethodTypeNumber { get; set; } = 1;
        public Guid? CustomerId { get; set; }

        //in case to had to add to your order list of order items use the next Line below
        public ICollection<OrderItemCreateDto> OrderItems { get; set; } = new HashSet<OrderItemCreateDto>();

    }
}

