namespace Horas.Api.Dtos.ProductWishlist
{
    public class ProductWishlistUpdateDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid WishListId { get; set; }
    }
}
