namespace Horas.Api.Dtos.Product
{
    public class ProductResDto : ProductCreateDto
    {
        public Guid Id { get; set; }
        public int? Rating { get; set; }
        public string[]? SupplierNames { get; set; }
    }
}
