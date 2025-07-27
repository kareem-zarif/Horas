
using Horas.Domain.Interfaces.IRepos;

namespace Horas.Data.Repos
{
    public class PaymentMethodRepo : BaseRepo<PaymentMethod>, IPaymentMethodRepo
    {
        public PaymentMethodRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        protected override IQueryable<PaymentMethod> IncludeNavProperties(DbSet<PaymentMethod> NavProperty)
        {
            return  _dbset
                .Include(c => c.Customer)
                .Include(o => o.Orders);
        }
    }
}
