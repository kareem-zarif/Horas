using Horas.Api.Dtos.CartItem;

namespace Horas.Api.Dtos.Cart
{
    public class CartResDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<CartItemResDto> CartItems { get; set; }
    }
}
