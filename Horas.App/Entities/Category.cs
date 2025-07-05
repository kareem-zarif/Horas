namespace Horas.Domain
{
    public class Category : BaseEnt
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //nav : use ICollection as not need indexing in IList
        public virtual ICollection<SubCategory> SubCategories { get; set; } = new HashSet<SubCategory>();
    }
}
