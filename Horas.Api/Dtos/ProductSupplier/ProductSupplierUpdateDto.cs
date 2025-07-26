namespace Horas.Api.Dtos.ProductSupplier
{
    public class ProductSupplierUpdateDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
