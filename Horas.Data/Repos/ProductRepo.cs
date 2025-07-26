
using Horas.Domain.Interfaces.IRepos;

namespace Horas.Data.Repos
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Product> IncludeNavProperties(DbSet<Product> NavProperty)
        {
            return _dbset
                .Include(x => x.Reviews)
                .Include(x => x.ProductSuppliers);
        }
    }
}
