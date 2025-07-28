
namespace Horas.Domain
{
    public class Category : IBaseEnt
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //nav : use ICollection as not need indexing in IList
        public virtual ICollection<SubCategory> SubCategories { get; set; } = new HashSet<SubCategory>();

        //IbaseEnt
        public Guid Id { get; set; }
        public bool IsExist { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
