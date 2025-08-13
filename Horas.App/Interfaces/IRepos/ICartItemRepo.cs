namespace Horas.Domain.Interfaces.IRepos
{
    public interface ICartItemRepo : IBaseRepo<CartItem>
    {
        Task<CartItem> GetByCartIdByProduct(Guid cartId, Guid productId);
        Task<IList<CartItem>> GetByCartIdAsync(Guid cartId);
    }
}
