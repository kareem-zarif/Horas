namespace Horas.Api.Dtos.CartItem
{
    public class CartItemCreateDto
    {
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public int Quantity { get; set; }

        //public string ProductName { get; set; }
        //public decimal Price { get; set; }
    }
}
