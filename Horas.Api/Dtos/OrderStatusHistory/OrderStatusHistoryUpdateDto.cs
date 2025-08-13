namespace Horas.Api.Dtos.OrderStatusHistory
{
    public class OrderStatusHistoryUpdateDto
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid OrderId { get; set; }
    }
}
