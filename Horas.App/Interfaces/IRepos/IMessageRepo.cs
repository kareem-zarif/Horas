

namespace Horas.Domain.Interfaces.IRepos
{
    public interface IMessageRepo : IBaseRepo<Message>
    {
        Task<List<Message>> GetBySupplierId(Guid suppId);
    }
}
