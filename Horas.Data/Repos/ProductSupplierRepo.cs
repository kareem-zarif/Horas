namespace Horas.Data.Repos
{
    public class ProductSupplierRepo : BaseRepo<ProductSupplier>, IProductSupplierRepo
    {
        public ProductSupplierRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        override protected IQueryable<ProductSupplier> IncludeNavProperties(DbSet<ProductSupplier> NavProperty)
        {
            return _dbset.Include(x => x.Product).Include(x => x.Supplier);
        }
    }
}
