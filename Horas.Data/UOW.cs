
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
        private ISubCategoryRepo _subCategoryRepo;
        private IWishListRepo _wishListRepo;
        private IOrderRepo _orderRepo;
        private IOrderItemRepo _orderItemRepo;
        private IOrderStatusHIstoryRepo _orderStatusHIstoryRepo;
        private IPaymentMethodRepo _paymentMethodRepo;
        private IProductWishlistRepo _productWishlistRepo;
        public UOW(HorasDBContext context)
        {
            _context = context;
        }

        //get property => make repo instance when only called 
        public IProductRepo PrdouctRepository => _productRepo ??= new ProductRepo(_context);
        public ISubCategoryRepo SubCategoryRepository => _subCategoryRepo ??= new SubCategoryRepo(_context);
        public IWishListRepo WishListRepository => _wishListRepo ??= new WishListRepo(_context);
        public IOrderRepo OrderRepository => _orderRepo ??= new OrderRepo(_context);
        public IOrderItemRepo OrderItemRepository => _orderItemRepo ??=new OrderItemRepo(_context);
        public IPaymentMethodRepo PaymentMethodRepository => _paymentMethodRepo ??= new PaymentMethodRepo(_context);

        public IProductWishlistRepo ProductWishListRepository => _productWishlistRepo ??= new ProductWishListRepo(_context);

        public IOrderStatusHIstoryRepo OrderStatusHistoryRepository => _orderStatusHIstoryRepo ??= new OrderStatusHistoryRepo(_context);

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
