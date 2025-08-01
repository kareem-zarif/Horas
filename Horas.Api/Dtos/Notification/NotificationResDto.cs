

namespace Horas.Api.Dtos.Notification
{
    public class NotificationResDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public List<CustomerReadDto> Customers { get; set; }
    }
}
