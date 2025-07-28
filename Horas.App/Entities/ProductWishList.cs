namespace Horas.Domain.Entities
{
    public class ProductWishList : IBaseEnt
    {
        public virtual Guid ProductId { get; set; }
        public virtual Guid WishListId { get; set; }
        //nav
        public virtual Product Product { get; set; }
        public virtual Wishlist WishList { get; set; }
        //----------------------------------IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
