namespace Horas.Api.Dtos.ProductSupplier
{
    public class ProductSupplierResDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double PricePerPiece { get; set; }
        public string FactoryName { get; set; }
    }
}
