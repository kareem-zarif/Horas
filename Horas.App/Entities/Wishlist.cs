namespace Horas.Domain
{
    public class Wishlist : IBaseEnt
    {
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual ICollection<ProductWishList> ProductWishLists { get; set; } = new HashSet<ProductWishList>();
        //----------------------------------IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
