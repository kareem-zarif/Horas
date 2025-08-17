namespace Horas.Data
{
    public class UOW : IUOW
    {
        #region shafie_UOW
        //public IBaseRepo<Department> DepartmentRepoo
        //{
        //    get
        //    {
        //        if (_departmentRepo == null)
        //            return new BaseRepo<Department>(_context);
        //        return _departmentRepo;
        //    }
        //}
        #endregion
        private readonly HorasDBContext _context;
        private IProductRepo _productRepo;
        private ICategoryRepo _categoryRepo;
        private ISubCategoryRepo _subCategoryRepo;
        private ICustomerRepo _customerRepo;
        private INotificationRepo _notificationRepo;
        private IMessageRepo _messageRepo;
        private IReviewRepo _reviewRepo;
        private IWishListRepo _wishListRepo;
        private IOrderRepo _orderRepo;
        private IOrderItemRepo _orderItemRepo;
        private IOrderStatusHIstoryRepo _orderStatusHIstoryRepo;
        private IPaymentMethodRepo _paymentMethodRepo;
        private IProductWishlistRepo _productWishlistRepo;
        private ICartRepo _cartRepo;
        private ICartItemRepo _cartItemRepo;
        private ISupplierRepo _supplierRepo;
        private IAddressRepo _addressRepo;
        private IProductSupplierRepo _productSupplierRepo;
        private IPersonNotificationRepo _personNotificationRepo;


        public UOW(HorasDBContext context)
        {
            _context = context;
        }

        //get property => make repo instance when only called 
        public IProductRepo ProductRepository => _productRepo ??= new ProductRepo(_context);
        public ICategoryRepo CategoryRepository => _categoryRepo ??= new CategoryRepo(_context);
        public ISubCategoryRepo SubCategoryRepository => _subCategoryRepo ??= new SubCategoryRepo(_context);
        public ICustomerRepo CustomerRepository => _customerRepo ??= new CustomerRepo(_context);
        public INotificationRepo NotificationRepository => _notificationRepo ??= new NotificationRepo(_context);
        public IWishListRepo WishListRepository => _wishListRepo ??= new WishListRepo(_context);
        public IOrderRepo OrderRepository => _orderRepo ??= new OrderRepo(_context);
        public IOrderItemRepo OrderItemRepository => _orderItemRepo ??=new OrderItemRepo(_context);
        public IPaymentMethodRepo PaymentMethodRepository => _paymentMethodRepo ??= new PaymentMethodRepo(_context);
        public IProductWishlistRepo ProductWishListRepository => _productWishlistRepo ??= new ProductWishListRepo(_context);
        public IOrderStatusHIstoryRepo OrderStatusHistoryRepository => _orderStatusHIstoryRepo ??= new OrderStatusHistoryRepo(_context);
        public ICartRepo CartRepository => _cartRepo ??= new CartRepo(_context);
        public ICartItemRepo CartItemRepository => _cartItemRepo ??= new CartItemRepo(_context);
        public ISupplierRepo SupplierRepository => _supplierRepo ??= new SupplierRepo(_context);
        public IAddressRepo AddressRepository => _addressRepo ??= new AddressRepo(_context);
        public IProductSupplierRepo ProductSupplierRepository => _productSupplierRepo ??= new ProductSupplierRepo(_context);
        public IMessageRepo MessageRepository => _messageRepo ??= new MessageRepo(_context);
        public IReviewRepo ReviewRepository => _reviewRepo ??= new ReviewRepo(_context);
        public IPersonNotificationRepo PersonNotificationRepository => _personNotificationRepo ??= new PersonNotificationRepo(_context);
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose() //call GC finalizer and prevent derived classes to call it again
        {
            _context.Dispose();
        }
    }
}
