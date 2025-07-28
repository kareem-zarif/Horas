namespace Horas.Domain
{
    public class Supplier : Person
    {
        //prebuilt questions will be in json-file
        public string FactoryName { get; set; }
        public string? Description { get; set; }
        public string? IFactoryPicPath { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankAccountNumber { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime? BlockUntil { get; set; } //nullable value type and not need write it is required (false) in fluent api
        //nav
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; } = new HashSet<ProductSupplier>();
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
        public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();

    }
}
