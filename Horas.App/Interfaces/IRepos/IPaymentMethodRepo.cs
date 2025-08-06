namespace Horas.Domain.Interfaces.IRepos
{
    public interface IPaymentMethodRepo :IBaseRepo<PaymentMethod>
    {
            Task<PaymentMethod> GetAsync(Expression<Func<PaymentMethod, bool>> filter);
    }
}
