

using Horas.Api.Dtos.CustomerNotification;

namespace Horas.Api.Dtos.Notification
{
    public class NotificationResDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
      
        public List<PersonNotificationRespDto> Customers { get; set; }
    }
}
