namespace Horas.Domain.Interfaces.IRepos
{
    public interface IWishListRepo : IBaseRepo<Wishlist>
    {
        Task<Wishlist> GetByCustomerIdAsync(Guid custId);
    }
}
