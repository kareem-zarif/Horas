namespace Horas.Api.Dtos.OrderItem
{
    public class OrderItemResDto:OrderItemCreateDto
    {
        public Guid Id { get; set; } 
        public string? ProductName { get; set; }
    }
}
