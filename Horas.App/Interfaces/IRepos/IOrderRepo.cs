
namespace Horas.Domain.Interfaces.IRepos
{
    public interface IOrderRepo:IBaseRepo<Order>
    {
        Task<Order> GetAsync(Expression<Func<Order, bool>> filter, string includeProperties = null);
        Task<IEnumerable<Order>> GetAllAsync(
    Func<IQueryable<Order>, IQueryable<Order>> include = null
);
    }
}
