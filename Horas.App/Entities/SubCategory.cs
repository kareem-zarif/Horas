namespace Horas.Domain
{
    public class SubCategory : BaseEnt
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        //nav 
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>(); 

    }
}
