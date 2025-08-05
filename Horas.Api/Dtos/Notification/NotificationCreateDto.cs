

namespace Horas.Api.Dtos.Notification
{
    public class NotificationCreateDto
    {
        [Required,MaxLength(255)]
        public string Message { get; set; }
      //  public bool IsRead { get; set; } = false;
    }
}
