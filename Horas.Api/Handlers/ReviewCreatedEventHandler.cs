using Horas.Domain.Events;
using MediatR;

namespace Horas.Application.Handlers
{
    public class ReviewCreatedEventHandler : INotificationHandler<ReviewCreatedEvent>
    {
        private readonly IUOW _uow;

        public ReviewCreatedEventHandler(IUOW uow)
        {
            _uow = uow;
        }

        public async Task Handle(ReviewCreatedEvent notification, CancellationToken cancellationToken)
        {
            var review = await _uow.ReviewRepository.GetAsync(notification.ReviewId);
            if (review == null) return;

            var product = await _uow.ProductRepository.GetAsync(review.ProductId);
            if (product == null) return;

            var productSuppliers = await _uow.ProductSupplierRepository
                .GetAllAsync(ps => ps.ProductId == product.Id);

            var supplierIds = productSuppliers.Select(ps => ps.SupplierId).ToList();

            var notificationEntity = new Notification
            {
                Message = $"  Your Product has been reviewed  {product.Name} rating {review.Rating}",
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
