
using Microsoft.AspNetCore.Identity;
namespace Horas.Domain
{
    public class Person : IdentityUser<Guid>, IBaseEnt
    {

        //authentication properties
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        //nav
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        public virtual ICollection<PersonNotification> PersonNotifications { get; set; } = new HashSet<PersonNotification>();


        //----------------------------------IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
