

namespace Horas.Api.Dtos.Notification
{
    public class NotificationUpdateDto
    {
        public Guid Id { get; set; }
        [Required, MaxLength(255)]
        public string Message { get; set; }
       // public bool IsRead { get; set; } = false;
    }
}
