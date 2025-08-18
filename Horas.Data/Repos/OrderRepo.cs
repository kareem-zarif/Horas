
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
                .Include(x => x.StatusHistories)
                .Include(oi => oi.OrderItems)
                    .ThenInclude(x => x.Product)
                 .AsSplitQuery();

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


        }

        public Task<IEnumerable<Order>> GetAllAsync(Func<IQueryable<Order>, IQueryable<Order>> include = null)
        {
            throw new NotImplementedException();
        }
    }
}
