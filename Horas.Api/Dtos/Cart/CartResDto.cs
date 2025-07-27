

namespace Horas.Api.Dtos.Cart
{
    public class CartResDto
    {
      public Guid id { set; get; }

        public List<CartItemResDto> CartItems { get; set; } = new();
    }
}
