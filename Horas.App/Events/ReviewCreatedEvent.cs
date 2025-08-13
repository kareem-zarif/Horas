using MediatR;

namespace Horas.Domain.Events
{
    public class ReviewCreatedEvent : INotification
    {
        public Guid ReviewId { get; }

        public ReviewCreatedEvent(Guid reviewId)
        {
            ReviewId = reviewId;
      }
    }
}
