namespace Horas.Data.Repos
{
    public class OrderItemRepo : BaseRepo<OrderItem>, IOrderItemRepo
    {
        public OrderItemRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<OrderItem>> GetAllBySupplierIdAsync(Guid suppId)
        {
            try
            {
                var found = await _dbset
                       .Include(x => x.Product)
                            .ThenInclude(x => x.ProductSuppliers)
                                .ThenInclude(x => x.Supplier)
                        .AsSplitQuery()
                                .Where(x => x.Product.ProductSuppliers.Any(x => x.SupplierId == suppId)).ToListAsync();

                return found;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.InnerException}");
                return null;
            }
        }

        protected override IQueryable<OrderItem> IncludeNavProperties(DbSet<OrderItem> NavProperty)
        {

            return _dbset.
                    Include(p => p.Product)
                   .Include(o => o.Order)
                   ;
        }

    }
}