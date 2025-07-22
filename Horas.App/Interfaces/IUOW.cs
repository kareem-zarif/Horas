using Horas.Domain.Interfaces.IRepos;

namespace Horas.Domain.Interfaces
{
    public interface IUOW : IDisposable
    {
        public IProductRepo PrdouctRepository { get; }

        Task<int> Complete();
    }
}
