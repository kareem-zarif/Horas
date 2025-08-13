using MediatR;
namespace Horas.Domain.Events
{
    public class OrderStatusChangedEvent : INotification
    {
        public Guid OrderId { get; }
        public OrderStatus Status { get; }

        public OrderStatusChangedEvent(Guid orderId, OrderStatus status)
        {
            OrderId = orderId;
            Status = status;
        }
    }
}
