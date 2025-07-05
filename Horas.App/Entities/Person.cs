
namespace Horas.Domain
{
    public abstract class Person : BaseEnt
    {
        //not write password and email attributes :: is built in IdentityFramork we will use 
        public string UserName { get; set; }

        [Required, MinLength(11), MaxLength(13), Phone, RegularExpression(@"^(010|011|012|15)\d{8,10}$", ErrorMessage = "Phone number must start with 010, 011,015, or 012 and be between 11 and 13 digits")]
        public string Phone { get; set; }
        //nav
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
