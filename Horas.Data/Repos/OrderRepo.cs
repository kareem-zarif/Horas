namespace Horas.Data.Repos
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        public OrderRepo(HorasDBContext dbContext) : base(dbContext)
        {
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
            ;
        }
    }
}
