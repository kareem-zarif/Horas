namespace Horas.Domain
{
    public class OrderStatusHistory : IBaseEnt
    {
        public OrderStatus OrderStatus { get; set; } = OrderStatus.pending;
        //public DateTime ChangedAt { get; set; } = DateTime.UtcNow; //UtcNow better for deployment in any cloud without conflicts
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        //nav
        public virtual Order Order { get; set; }
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
