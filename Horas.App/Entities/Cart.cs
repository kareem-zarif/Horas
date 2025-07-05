namespace Horas.Domain
{
    public class Cart : BaseEnt
    {

        public Guid CustomerId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
