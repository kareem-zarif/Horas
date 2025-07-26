
using Microsoft.AspNetCore.Identity;
namespace Horas.Domain
{
    public  class Person : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //nav
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
