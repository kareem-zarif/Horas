namespace Horas.Domain.Interfaces
{
    public interface IUOW : IDisposable
    {
        public IProductRepo     PrdouctRepository { get; }
        public ISubCategoryRepo SubCategoryRepository { get; }

        public IWishListRepo    WishListRepository { get; }
        public IOrderRepo       OrderRepository { get; }
        public IOrderItemRepo   OrderItemRepository { get; }
        public IOrderStatusHIstoryRepo OrderStatusHistoryRepository { get; }
        public IPaymentMethodRepo      PaymentMethodRepository { get; }    
        public IProductWishlistRepo    ProductWishListRepository { get; }

        public ICartRepo CartRepository { get; }
        public ICartItemRepo CartItemRepository { get; }
        public ISupplierRepo SupplierRepository { get; }
        public IAddressRepo AddressRepository { get; }
        public IProductSupplierRepo ProductSupplierRepository { get; }

        Task<int> Complete();
    }
}
