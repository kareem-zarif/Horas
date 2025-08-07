using Horas.Domain.Events;
using MediatR;

namespace Horas.Application.Handlers
{
    public class OrderStatusChangedEventHandler : INotificationHandler<OrderStatusChangedEvent>
    {
        private readonly IUOW _uow;

        public OrderStatusChangedEventHandler(IUOW uow)
        {
            _uow = uow;
        }

        public async Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository.GetAsync(notification.OrderId);
            if (order == null || order.CustomerId == null) return;

            var notificationEntity = new Notification
            {
                Message = $"Your Order Status: {notification.Status}"
            };
            await _uow.NotificationRepository.CreateAsync(notificationEntity);

            var personNotification = new PersonNotification
            {
                PersonId = order.CustomerId.Value,
                NotificationId = notificationEntity.Id
            };
            await _uow.PersonNotificationRepository.CreateAsync(personNotification);

            await _uow.Complete();
        }
    }
}
