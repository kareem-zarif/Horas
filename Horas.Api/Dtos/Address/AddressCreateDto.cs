namespace Horas.Api.Dtos.Address
{
    public class AddressCreateDto
    {
        public Guid PersonId { get; set; }
        public string? Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; } = "Egypt";
    }
}
