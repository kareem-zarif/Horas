namespace Horas.Api.Dtos.CartItem
{
    public class CartItemUpdateDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public int Quantity { get; set; }

    }
}
