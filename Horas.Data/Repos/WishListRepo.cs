namespace Horas.Data.Repos
{
    public class WishListRepo : BaseRepo<Wishlist>, IWishListRepo
    {
        public WishListRepo(HorasDBContext dbContext) : base(dbContext)
        {
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
