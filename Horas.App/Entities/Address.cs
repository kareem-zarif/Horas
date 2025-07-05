namespace Horas.Domain
{
    public class Address : BaseEnt
    {
        public string? Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; } = "Egypt";
        public Guid PersonId { get; set; }
        //nav
        public virtual Person Person { get; set; }
    }
}
