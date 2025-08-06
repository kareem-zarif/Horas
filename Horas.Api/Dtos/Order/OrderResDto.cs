namespace Horas.Api.Dtos.Order
{
    public class OrderResDto 
    {
        public Guid Id { get; set; }
        public int? PaymentMethodName { get; set; }

        public string? CustomerName { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.pending;
        [Required]
        public Guid? PaymentMethodId { get; set; }
        public Guid? CustomerId { get; set; }
        public ICollection<OrderItemResDto> OrderItems { get; set; } = new HashSet<OrderItemResDto>();
    }
}
