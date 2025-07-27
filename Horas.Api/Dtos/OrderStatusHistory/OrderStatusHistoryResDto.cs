using Horas.Domain;

namespace Horas.Api.Dtos.OrderStatusHistory
{
    public class OrderStatusHistoryResDto
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime ChangedAt { get; set; }
        public Guid OrderId { get; set; }
    }
}
