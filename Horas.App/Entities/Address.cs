namespace Horas.Domain
{
    public class Address : IBaseEnt
    {
        public string? Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; } = "Egypt";
        public Guid PersonId { get; set; }
        public bool IsDefault { get; set; } = false;

        //nav
        public virtual Person Person { get; set; }

        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
