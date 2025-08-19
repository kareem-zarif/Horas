namespace Horas.Data.Repos
{
    public class ProductWishListRepo : BaseRepo<ProductWishList>, IProductWishlistRepo
    {
        public ProductWishListRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        public virtual async Task<ProductWishList> GetByWishlistIdByProductId(Guid wishlistId, Guid productId)
        {
            var found = await _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.WishListId == wishlistId && x.ProductId == productId);


            return found;
        }
        public virtual async Task<ProductWishList> DeleteByCustomerIdByProductId(Guid wishlistId, Guid productId)
        {
            var found = await _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.WishListId == wishlistId && x.ProductId == productId);

            _dbset.Remove(found);

            return found;
        }
        public virtual async Task<IList<ProductWishList>> GetAllByWishlistId(Guid wishlistId)
        {
            var found = await _dbset.AsNoTracking().Where(x => x.WishListId == wishlistId).ToListAsync();


            return found;
        }
        public virtual async Task<IList<ProductWishList>> ClearByWishlistId(Guid wishlistId)
        {
            var foundList = await _dbset.AsNoTracking().Where(x => x.WishListId == wishlistId).ToListAsync();

            _dbset.RemoveRange(foundList);

            return foundList;
        }
        protected override IQueryable<ProductWishList> IncludeNavProperties(DbSet<ProductWishList> NavProperty)
        {
            return _dbset
                .Include(x => x.Product);
        }
    }
}
