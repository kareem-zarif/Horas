using MediatR;

namespace Horas.Domain.Events
{
    public class ProductChangedEvent : INotification
    {
        public Guid ProductId { get; set; }
        public string Message { get; set; }

        public ProductChangedEvent(Guid productId, string message)
        {
            ProductId = productId;
            Message = message;
        }
    }
}
