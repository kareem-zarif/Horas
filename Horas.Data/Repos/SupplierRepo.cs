namespace Horas.Data.Repos
{
    public class SupplierRepo : BaseRepo<Supplier>, ISupplierRepo
    {
        public SupplierRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Supplier> IncludeNavProperties(DbSet<Supplier> NavProperty)
        {
            return _dbset
                .Include(x => x.ProductSuppliers)
                    .ThenInclude(x => x.Product)
                .Include(x => x.Addresses)
                .AsSplitQuery() //solve cartisian problem
                ;
        }
    }
}
