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
                   .Include(o => o.Order)
                   ;
        }

    }
}