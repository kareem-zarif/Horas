namespace Horas.Domain
{
    public class CartItem : IBaseEnt
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        //nav
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
