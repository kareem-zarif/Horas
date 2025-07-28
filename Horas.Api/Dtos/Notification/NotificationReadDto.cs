namespace Horas.Api.Dtos.Notification
{
    public class NotificationReadDto
    {
        public Guid id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
