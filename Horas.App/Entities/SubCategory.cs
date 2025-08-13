namespace Horas.Domain
{
    public class SubCategory : IBaseEnt
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        //nav 
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        //----------------------------------IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
