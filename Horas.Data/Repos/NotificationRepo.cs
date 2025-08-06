

namespace Horas.Data.Repos
{
    class NotificationRepo:BaseRepo<Notification>,INotificationRepo
    {
        public NotificationRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Notification> IncludeNavProperties(DbSet<Notification> dbSet)
        {
            return _dbset.Include(x => x.PersonNotifications);
               
               
        }
    }

}
