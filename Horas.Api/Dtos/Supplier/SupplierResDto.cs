namespace Horas.Api.Dtos.Supplier
{
    public class SupplierResDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string FactoryName { get; set; }
        public string Description { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public ICollection<ProductResDto> Products { get; set; }
    }
}
