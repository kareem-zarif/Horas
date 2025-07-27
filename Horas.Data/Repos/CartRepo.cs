namespace Horas.Data.Repos
{
    public class CartRepo : BaseRepo<Cart>, ICartRepo
    {
        public CartRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Cart> IncludeNavProperties(DbSet<Cart> NavProperty)
        {
            return _dbset.Include(x=>x.CartItems);
        }
    }
}