namespace Horas.Api.Dtos.Supplier
{
    public class SupplierUpdateDto
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Phone { get; set; }
        [Required]
        public string FactoryName { get; set; }
        public string? Description { get; set; }

    }
}
