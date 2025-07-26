using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horas.Domain.Interfaces.IRepos;

namespace Horas.Data.Repos
{
    public class AddressRepo : BaseRepo<Address>, IAddressRepo
    {
        public AddressRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Address> IncludeNavProperties(DbSet<Address> NavProperty)
        {
            return _dbset.Include(x => x.Person);
        }
    }
    }
