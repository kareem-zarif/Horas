namespace Horas.Data.Repos
{
    public class WishListRepo : BaseRepo<Wishlist>, IWishListRepo
    {
        public WishListRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        public async Task<Wishlist> GetByCustomerIdAsync(Guid custId)
        {
            try
            {
                var found = await _dbset
                        //.Include(x => x.Customer)
                        .Include(x => x.ProductWishLists)
                            .ThenInclude(x => x.Product)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(x => x.CustomerId == custId);

                return found;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        protected override IQueryable<Wishlist> IncludeNavProperties(DbSet<Wishlist> NavProperty)
        {
            return _dbset.Include(x => x.Customer)
                         .Include(x => x.ProductWishLists)
                            .ThenInclude(x => x.Product)
                            .AsSplitQuery();

        }
    }
}
