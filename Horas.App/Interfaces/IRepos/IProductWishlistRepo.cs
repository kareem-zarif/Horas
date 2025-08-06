
namespace Horas.Domain.Interfaces.IRepos
{
    public interface IProductWishlistRepo : IBaseRepo<ProductWishList>
    {
        Task<ProductWishList> GetByWishlistIdByProductId(Guid wishlistId, Guid productId);
        Task<ProductWishList> DeleteByCustomerIdByProductId(Guid wishlistId, Guid productId);
        Task<IList<ProductWishList>> ClearByWishlistId(Guid wishlistId);
        Task<IList<ProductWishList>> GetAllByWishlistId(Guid wishlistId);
    }
}
