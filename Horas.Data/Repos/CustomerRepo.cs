

namespace Horas.Data.Repos
{
    public class CustomerRepo : BaseRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(HorasDBContext dbContext) : base(dbContext)
        {

        }
        protected override IQueryable<Customer> IncludeNavProperties(DbSet<Customer> dbSet)
        {
            return _dbset
                .Include(c => c.Orders)
                .Include(c => c.Cart)
                .Include(c => c.Wishlist)
                .Include(c => c.PaymentMethods)
                .Include(c => c.Messages)
                .Include(c => c.Reviews)
                .Include(c => c.Reports)
                .Include(c => c.Notifications)
                ;
        }
    }
}

