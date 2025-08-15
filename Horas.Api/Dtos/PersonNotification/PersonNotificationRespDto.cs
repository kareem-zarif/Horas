namespace Horas.Api.Dtos.CustomerNotification
{
    public class PersonNotificationRespDto
    {
        public Guid PersonId { get; set; }
        public Guid NotificationId { get; set; }
        public Guid Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsEnable { get; set; }
        public bool? IsRead { get; set; }

    }
}
