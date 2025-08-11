namespace Horas.Data.Repos
{
    public class CartItemRepo : BaseRepo<CartItem>, ICartItemRepo
    {
        public CartItemRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        public virtual async Task<CartItem> GetByCartIdByProduct(Guid cartId, Guid productId)
        {
            var found = await _dbset
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);

            return found;
        }

        public async Task<IList<CartItem>> GetByCartIdAsync(Guid cartId)
        {
            return await _dbset
                .Include(x => x.Product)
                .Where(x => x.CartId == cartId)
                .ToListAsync();
        }
        protected override IQueryable<CartItem> IncludeNavProperties(DbSet<CartItem> NavProperty)
        {
            return _dbset.Include(x => x.Cart).Include(x => x.Product);
        }
    }
}