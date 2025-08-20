
namespace Horas.Domain.Interfaces.IRepos
{
    public interface IOrderItemRepo : IBaseRepo<OrderItem>
    {
        Task<IList<OrderItem>> GetAllBySupplierIdAsync(Guid suppId);
    }
}
