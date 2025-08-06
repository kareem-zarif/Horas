
using Horas.Domain.Interfaces.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Horas.Data.Repos
{
    public class PaymentMethodRepo : BaseRepo<PaymentMethod>, IPaymentMethodRepo
    {
        private readonly HorasDBContext _context;
        public PaymentMethodRepo(HorasDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }


        protected override IQueryable<PaymentMethod> IncludeNavProperties(DbSet<PaymentMethod> NavProperty)
        {
            return  _dbset
                .Include(c => c.Customer)
                .Include(o => o.Orders);
        }

        public async Task<PaymentMethod> GetAsync(Expression<Func<PaymentMethod, bool>> filter)
        {
            return await _context.Set<PaymentMethod>().FirstOrDefaultAsync(filter);
        }

    }
}
