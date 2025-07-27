

namespace Horas.Domain.Interfaces
{
    public interface IUOW : IDisposable
    {
        public IProductRepo ProductRepository { get; }
        public ICategoryRepo CategoryRepository { get; }
        public ISubCategoryRepo SubCategoryRepository { get; }
        public ICustomerRepo CustomerRepository { get; }
        public INotificationRepo NotificationRepository { get; }

        public IMessageRepo MessageRepository { get; }

        public IReviewRepo ReviewRepository { get; }
        Task<int> Complete();
    }
}
