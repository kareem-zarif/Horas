namespace Horas.Domain.Interfaces.IRepos
{
    public interface IPaymentMethodRepo : IBaseRepo<PaymentMethod>
    {
        Task<PaymentMethod> GetAsyncByExpression(Expression<Func<PaymentMethod, bool>> filter);
    }
}
