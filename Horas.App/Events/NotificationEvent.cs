using MediatR;

namespace Horas.Domain.Events
{
    public class NotificationEvent : INotification
    {
        public string Message { get; set; }
        public Guid? PersonId { get; set; }
        public List<Guid> PeopleIds { get; set; }
        public NotificationEvent(string message, Guid personId)
        {
            Message = message;
            PersonId = personId;
        }
        public NotificationEvent(string message, List<Guid> peopleIds)
        {
            Message = message;
            PeopleIds = peopleIds;
        }
    }

}
