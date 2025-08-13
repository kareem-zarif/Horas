namespace Horas.Api.Dtos.OrderStatusHistory
{
    public class OrderStatusHistoryCreateDto
    {
        public OrderStatus OrderStatus { get; set; }
        public Guid OrderId { get; set; }
    }
}
