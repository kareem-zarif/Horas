namespace Horas.Data.Repos
{
    public class SupplierRepo : BaseRepo<Supplier>, ISupplierRepo
    {
        public SupplierRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Supplier> IncludeNavProperties(DbSet<Supplier> NavProperty)
        {
            return _dbset.Include(x => x.ProductSuppliers).Include(x => x.Addresses);
        }
    }
}
