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
        public UOW(HorasDBContext context)
        {
            _context = context;
        }

        //get property => make repo instance when only called 
        public IProductRepo PrdouctRepository => _productRepo ??= new ProductRepo(_context);



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
