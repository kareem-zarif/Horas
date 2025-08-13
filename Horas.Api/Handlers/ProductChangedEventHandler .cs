using Horas.Domain.Events;
using MediatR;

namespace Horas.Application.Handlers
{
    public class ProductChangedEventHandler : INotificationHandler<ProductChangedEvent>
    {
        private readonly IUOW _uow;

        public ProductChangedEventHandler(IUOW uow)
        {
            _uow = uow;
        }

        public async Task Handle(ProductChangedEvent notification, CancellationToken cancellationToken)
        {
            var product = await _uow.ProductRepository.GetAsync(notification.ProductId);
            if (product == null) return;

            var productSuppliers = await _uow.ProductSupplierRepository
                .GetAllAsync(ps => ps.ProductId == product.Id);

            var supplierIds = productSuppliers.Select(ps => ps.SupplierId).ToList();

            var notificationEntity = new Notification
            {
                Message = notification.Message
            };
            await _uow.NotificationRepository.CreateAsync(notificationEntity);

            foreach (var supplierId in supplierIds)
            {
                var personNotification = new PersonNotification
                {
                    PersonId = supplierId,
                    NotificationId = notificationEntity.Id
                };
                await _uow.PersonNotificationRepository.CreateAsync(personNotification);
            }

            await _uow.Complete();
        }
    }
}
