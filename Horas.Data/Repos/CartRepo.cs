namespace Horas.Data.Repos
{
    public class CartRepo : BaseRepo<Cart>, ICartRepo
    {
        public CartRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        public async Task<Cart> GetByCustomerIdAsync(Guid custId)
        {
            var found = await _dbset
                .Include(x => x.CartItems)
                    .ThenInclude(x => x.Product)
                    .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.CustomerId == custId);

            return found;
        }

        protected override IQueryable<Cart> IncludeNavProperties(DbSet<Cart> NavProperty)
        {
            return _dbset.Include(x => x.CartItems);
        }
    }
}