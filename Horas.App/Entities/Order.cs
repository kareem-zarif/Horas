namespace Horas.Domain
{
    public class Order : BaseEnt
    {
        public double TotalAmount { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? CustomerId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<OrderStatusHistory> StatusHistories { get; set; } = new HashSet<OrderStatusHistory>();
    }
}
