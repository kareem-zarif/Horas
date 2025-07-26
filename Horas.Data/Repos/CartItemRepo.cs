using Horas.Domain.Interfaces.IRepos;

namespace Horas.Data.Repos
{
    public class CartItemRepo : BaseRepo<CartItem>, ICartItemRepo
    {
        public CartItemRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<CartItem> IncludeNavProperties(DbSet<CartItem> NavProperty)
        {
            return _dbset.Include(x => x.Cart).Include(x => x.Product);
        }
    }
}