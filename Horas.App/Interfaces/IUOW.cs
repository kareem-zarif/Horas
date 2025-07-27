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
        Task<int> Complete();
    }
}
