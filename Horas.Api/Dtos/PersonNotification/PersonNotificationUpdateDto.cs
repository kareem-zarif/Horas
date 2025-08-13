namespace Horas.Api.Dtos.CustomerNotification
{
    public class PersonNotificationUpdateDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid NotificationId { get; set; }
        public bool? IsEnable { get; set; }
        public bool? IsRead { get; set; }
    }
}
