using Horas.Domain.Interfaces.IRepos;

namespace Horas.Data.Repos
{
    public class SubCategoryRepo : BaseRepo<SubCategory>, ISubCategoryRepo
    {
        public SubCategoryRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }


        protected override IQueryable<SubCategory> IncludeNavProperties(DbSet<SubCategory> NavProperty)
        {
            return _dbset.Include(x => x.Category);
        }
    }
}
