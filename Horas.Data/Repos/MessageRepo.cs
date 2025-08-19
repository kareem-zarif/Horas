

namespace Horas.Data.Repos
{
    public class MessageRepo : BaseRepo<Message>, IMessageRepo
    {
        public MessageRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Message>> GetBySupplierId(Guid suppId)
        {
            try
            {
                var found = await _dbset
                        .Include(x => x.Customer)
                        .Include(x => x.Supplier)
                        .Where(x => x.SupplierId == suppId)
                        .ToListAsync();

                return found;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.InnerException}");
                return null;
            }
        }
        protected override IQueryable<Message> IncludeNavProperties(DbSet<Message> dbSet)
        {
            return _dbset
                .Include(c => c.Customer)
                .Include(c => c.Supplier);
        }
    }
}

