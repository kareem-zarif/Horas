using System.ComponentModel.DataAnnotations.Schema;

namespace Horas.Domain
{
    public class CartItem : BaseEnt
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        //nav
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
