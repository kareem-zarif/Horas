
namespace Horas.Domain.Entities
{
    public class ProductSupplier :BaseEnt
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }
        //nav
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
