namespace Horas.Domain.Interfaces.IRepos
{
    public interface ICartRepo : IBaseRepo<Cart>
    {
        Task<Cart> GetByCustomerIdAsync(Guid custId);
    }
}
