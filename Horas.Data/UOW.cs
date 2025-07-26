using Horas.Data.Repos;
using Horas.Domain.Interfaces;
using Horas.Domain.Interfaces.IRepos;

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
        private ICartRepo _cartRepo;
        private ICartItemRepo _cartItemRepo;
        private ISupplierRepo _supplierRepo;
        private IAddressRepo _addressRepo;
        private IProductSupplierRepo _productSupplierRepo;

        public UOW(HorasDBContext context)
        {
            _context = context;
        }

        //get property => make repo instance when only called 
        public IProductRepo PrdouctRepository => _productRepo ??= new ProductRepo(_context);
        public ISubCategoryRepo SubCategoryRepository => _subCategoryRepo ??= new SubCategoryRepo(_context);
        public ICartRepo CartRepository => _cartRepo ??= new CartRepo(_context);
        public ICartItemRepo CartItemRepository => _cartItemRepo ??= new CartItemRepo(_context);
        public ISupplierRepo SupplierRepository => _supplierRepo ??= new SupplierRepo(_context);
        public IAddressRepo AddressRepository => _addressRepo ??= new AddressRepo(_context);
        public IProductSupplierRepo ProductSupplierRepository => _productSupplierRepo ??= new ProductSupplierRepo(_context);

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
