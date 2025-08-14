namespace Horas.Api.Dtos.OrderItem
{
    public class OrderItemCreateDto 
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; } 
        public bool IsSample { get; set; }
        public Guid ProductId { get; set; }
        public Guid? OrderId { get; set; }
    }
}
