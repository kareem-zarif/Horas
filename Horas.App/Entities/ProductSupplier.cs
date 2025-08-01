
namespace Horas.Domain.Entities
{
    public class ProductSupplier : IBaseEnt
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }
        //nav
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
        //----------------------------------IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
