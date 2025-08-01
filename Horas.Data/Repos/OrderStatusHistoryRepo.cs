using Horas.Domain.Interfaces.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horas.Data.Repos
{
    public class OrderStatusHistoryRepo : BaseRepo<OrderStatusHistory>, IOrderStatusHIstoryRepo
    {
        public OrderStatusHistoryRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        protected override IQueryable<OrderStatusHistory> IncludeNavProperties(DbSet<OrderStatusHistory> NavProperty)
        {
            return _dbset.Include(o => o.Order);
        }
    }

}