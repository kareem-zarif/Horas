using Horas.Domain.Interfaces.IRepos;

namespace Horas.Domain.Interfaces
{
    public interface IUOW : IDisposable
    {
        public IProductRepo PrdouctRepository { get; }
        public ISubCategoryRepo SubCategoryRepository { get; }
        public ICartRepo CartRepository { get; }
        public ICartItemRepo CartItemRepository { get; }
        public ISupplierRepo SupplierRepository { get; }
        public IAddressRepo AddressRepository { get; }
        public IProductSupplierRepo ProductSupplierRepository { get; }
        Task<int> Complete();
    }
}
