namespace Horas.Domain
{
    public class Report : IBaseEnt
    {
        public string? Reason { get; set; }//additionally use exlict fluentApi isRequired(false) for refernce types as best practise 
        public Guid? CustomerId { get; set; }
        public Guid? SupplierId { get; set; }
        //nav
        public virtual Customer Customer { get; set; }
        public virtual Supplier Supplier { get; set; }
        //----------------------------------IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
