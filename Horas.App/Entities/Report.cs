namespace Horas.Domain
{
    public class Report : BaseEnt
    {
        public string? Reason { get; set; }//additionally use exlict fluentApi isRequired(false) for refernce types as best practise 
        public Guid? CustomerId { get; set; }
        public Guid? SupplierId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
