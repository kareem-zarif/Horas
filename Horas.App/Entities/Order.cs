namespace Horas.Domain
{
    public class Order : IBaseEnt
    {
        public double TotalAmount { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? CustomerId { get; set; }
        
        public OrderStatus OrderStatus { get; set; } // Added with Migration

        //nav
        public virtual Customer Customer { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<OrderStatusHistory> StatusStatusHistories { get; set; } = new HashSet<OrderStatusHistory>();
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
