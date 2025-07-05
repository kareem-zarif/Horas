namespace Horas.Domain
{
    public class OrderStatusHistory : BaseEnt
    {
        public OrderStatus OrderStatus { get; set; } = OrderStatus.pending;
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow; //UtcNow better for deployment in any cloud without conflicts
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        //nav
        public virtual Order Order { get; set; }
    }
}
