namespace Horas.Domain.Interfaces
{
    public interface IUOW : IDisposable
    {
        public IProductRepo ProductRepository { get; }
        public ICategoryRepo CategoryRepository { get; }

        public ISubCategoryRepo SubCategoryRepository { get; }
        public ICustomerRepo CustomerRepository { get; }
        public INotificationRepo NotificationRepository { get; }

        public IMessageRepo MessageRepository { get; }
        public IReviewRepo ReviewRepository { get; }
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
        public IPersonNotificationRepo PersonNotificationRepository { get; }


        Task<int> Complete();
    }
}
