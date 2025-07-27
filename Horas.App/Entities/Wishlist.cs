using Horas.Domain.Entities;

namespace Horas.Domain
{
    public class Wishlist : BaseEnt
    {
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual ICollection<ProductWishList> ProductWishLists { get; set; } = new HashSet<ProductWishList>();
    }
}
