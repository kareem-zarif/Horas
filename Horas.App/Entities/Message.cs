namespace Horas.Domain
{
    public class Message : BaseEnt
    {
        public string Body { get; set; }
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        [ForeignKey("Supplier")]
        public Guid? SupplierId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
