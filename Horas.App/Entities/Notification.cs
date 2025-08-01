
namespace Horas.Domain
{
    public class Notification : IBaseEnt
    {
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        //nav 
        public virtual ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
