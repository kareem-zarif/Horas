
using Horas.Data.Repos;
using Horas.Domain.Interfaces.IRepos;

namespace Horas.Data.Repos
{
    public class OrderItemRepo : BaseRepo<OrderItem>, IOrderItemRepo
    {
        public OrderItemRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

    
        protected override IQueryable<OrderItem> IncludeNavProperties(DbSet<OrderItem> NavProperty)
        {

            return _dbset.
                    Include(p => p.Product)
                   .Include(o => o.Order);
        }

    }
}