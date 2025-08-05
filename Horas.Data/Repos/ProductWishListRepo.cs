namespace Horas.Data.Repos
{
    public class ProductWishListRepo : BaseRepo<ProductWishList>, IProductWishlistRepo
    {
        public ProductWishListRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        protected override IQueryable<ProductWishList> IncludeNavProperties(DbSet<ProductWishList> NavProperty)
        {
            return _dbset.Include(x => x.Product);
        }
    }
}
