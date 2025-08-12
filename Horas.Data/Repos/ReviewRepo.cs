

namespace Horas.Data.Repos
{
    public class ReviewRepo : BaseRepo<Review>, IReviewRepo
    {
        public ReviewRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        protected override IQueryable<Review> IncludeNavProperties(DbSet<Review> dbSet)
        {
            return _dbset
                .Include(x => x.Customer)
                .Include(c => c.Product);
        }
    }
}
