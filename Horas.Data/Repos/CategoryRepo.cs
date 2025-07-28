

namespace Horas.Data.Repos
{
  public class CategoryRepo: BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        protected override IQueryable<Category> IncludeNavProperties(DbSet<Category> NavProperty)
        {
            return _dbset.Include(x => x.SubCategories);
        }
    
    }
}
