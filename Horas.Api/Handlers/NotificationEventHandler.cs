using Horas.Domain.Events;
using MediatR;

namespace Horas.Api.Handlers
{
    public class NotificationEventHandler : INotificationHandler<NotificationEvent>
    {
        private readonly IUOW _uow;

        public NotificationEventHandler(IUOW uow)
        {
            _uow = uow;
        }

        public async Task Handle(NotificationEvent notificationEvent, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Message = notificationEvent.Message,
            };

            var createdNotification = await _uow.NotificationRepository.CreateAsync(notification);
            await _uow.Complete();

            var personToNotify = new List<Guid>();

            if (notificationEvent.PersonId.HasValue)
                personToNotify.Add(notificationEvent.PersonId.Value);

            if (notificationEvent.PeopleIds != null && notificationEvent.PeopleIds.Any())
                personToNotify.AddRange(notificationEvent.PeopleIds);

            personToNotify = personToNotify.Distinct().ToList();

            foreach (var personId in personToNotify)
            {
                var personNotification = new PersonNotification
                {
                    PersonId = personId,
                    NotificationId = createdNotification.Id,
                   
                };

                await _uow.PersonNotificationRepository.CreateAsync(personNotification);
            }

            await _uow.Complete();
        }
    }
}


