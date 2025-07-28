namespace Horas.Api.Dtos.OrderStatusHistory
{
    public class OrderStatusHistoryResDto
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid OrderId { get; set; }
    }
}
