using Horas.Domain.Entities;
using Horas.Domain.Interfaces.IRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horas.Data.Repos
{
    public class ProductWishListRepo :BaseRepo<ProductWishList>, IProductWishlistRepo
    {
        public ProductWishListRepo(HorasDBContext dbContext) : base(dbContext)
    {
    }
    protected override IQueryable<ProductWishList> IncludeNavProperties(DbSet<ProductWishList> NavProperty)
    {
        return _dbset.Include(x => x.Product)
;


    }
}
}
