
using Horas.Domain.Interfaces.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Horas.Data.Repos
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private readonly HorasDBContext _context;
        public OrderRepo(HorasDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }


        protected override IQueryable<Order> IncludeNavProperties(DbSet<Order> NavProperty)
        {
            return _dbset
                .Include(x => x.Customer)
                .Include(x => x.PaymentMethod)
<<<<<<< HEAD
                .Include(oi => oi.OrderItems)
                .Include(x => x.StatusStatusHistories);
        }


        public async Task<Order> GetAsync(Expression<Func<Order, bool>> filter, string includeProperties = null)
        {
            IQueryable<Order> query = _context.Set<Order>();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync(filter);
=======
                .Include(oi => oi.OrderItems).
                 Include(s =>s.StatusHistories)
                ;
>>>>>>> origin/menna2
        }
    }
}
