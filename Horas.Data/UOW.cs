

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

        public IMessageRepo MessageRepository => _messageRepo ??= new MessageRepo(_context);

        public IReviewRepo ReviewRepository => _reviewRepo ??= new ReviewRepo(_context);
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
