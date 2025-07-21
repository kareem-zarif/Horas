using Horas.Domain.Interfaces.IRepos;

namespace Horas.Domain.Interfaces
{
    public interface IUOW
    {
        public IProductRepo PrdouctRepository { get; }
    }
}
