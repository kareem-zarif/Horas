namespace Horas.Domain
{
    public class Wishlist : BaseEnt
    {
        public Guid CustomerId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
