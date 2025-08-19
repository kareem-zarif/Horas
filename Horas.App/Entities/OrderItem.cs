namespace Horas.Domain
{
    public class OrderItem : IBaseEnt
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; } //price at time of purchase
        public bool IsSample { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }
        //nav
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        public virtual Supplier Supplier { get; set; }
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
