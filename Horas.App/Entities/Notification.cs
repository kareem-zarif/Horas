
namespace Horas.Domain
{
    public class Notification : IBaseEnt
    {
        public string Message { get; set; }

        //nav 
        public virtual ICollection<PersonNotification> PersonNotifications { get; set; } = new HashSet<PersonNotification>();
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
