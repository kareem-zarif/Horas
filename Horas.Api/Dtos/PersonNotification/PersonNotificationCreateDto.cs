namespace Horas.Api.Dtos.CustomerNotification
{
    public class PersonNotificationCreateDto
    {
        public Guid PersonId { get; set; }
        public Guid NotificationId { get; set; }
        public bool? IsEnable { get; set; } 

    }
}
