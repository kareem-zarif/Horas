namespace Horas.Api.Dtos.Supplier
{
    public class SupplierCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [Required]
        public string FactoryName { get; set; }
        public string? Description { get; set; }
    }
}
