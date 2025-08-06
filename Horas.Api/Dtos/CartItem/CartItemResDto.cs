namespace Horas.Api.Dtos.CartItem
{
    public class CartItemResDto
    {

        public Guid Id { get; set; }
        public int Quantity { get; set; }
        //public Guid ProductId { get; set; }
        //public string ProductName { get; set; }
        //public decimal PricePerPiece { get; set; }
        //public decimal PricePer100Piece { get; set; }
        //public List<string> ProductPicsPathes { get; set; }
        public ProductResDto Product { get; set; }
    }
}
