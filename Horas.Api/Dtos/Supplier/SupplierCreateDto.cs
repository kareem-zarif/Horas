namespace Horas.Api.Dtos.Supplier
{
    public class SupplierCreateDto
    {
        [Required]
        public string UserName { get; set; }
        public string Phone { get; set; }
        [Required]
        public string FactoryName { get; set; }
        public string? Description { get; set; }
    }
}
