

namespace Horas.Data.Repos
{
  public class MessageRepo:BaseRepo<Message>,IMessageRepo
    {
        public MessageRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        protected override IQueryable<Message> IncludeNavProperties(DbSet<Message> dbSet)
        {
            return _dbset
                .Include(c => c.Customer)
                .Include(c => c.Supplier);
        }
    }
}

