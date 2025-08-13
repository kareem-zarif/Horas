namespace Horas.Api.Dtos.Supplier
{
    public class SupplierUpdateDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string FactoryName { get; set; }
        public string? Description { get; set; }

    }
}
