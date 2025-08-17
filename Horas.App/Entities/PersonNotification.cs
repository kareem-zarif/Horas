

namespace Horas.Domain.Entities
{
    public class PersonNotification : IBaseEnt
    {
        [ForeignKey("Person")]
        public Guid PersonId { get; set; }
        [ForeignKey("Notification")]
        public Guid NotificationId { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsEnable { get; set; } = true;

        //nav
        public virtual Person Person { get; set; }
        public virtual Notification Notification { get; set; }
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        //public static implicit operator CustomerNotification(CustomerNotification v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
