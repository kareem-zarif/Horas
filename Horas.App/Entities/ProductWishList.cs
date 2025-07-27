namespace Horas.Domain.Entities
{
    public class ProductWishList:BaseEnt
    {
        public virtual Guid ProductId { get; set; }
        public virtual Guid WishListId { get; set; }
        //nav
        public virtual Product Product { get; set; }
        public virtual Wishlist WishList { get; set; }
    }
}
