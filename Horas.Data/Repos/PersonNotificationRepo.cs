namespace Horas.Data.Repos
{
    public class PersonNotificationRepo:BaseRepo<PersonNotification>, IPersonNotificationRepo
    {
        public PersonNotificationRepo(HorasDBContext dbContext) : base(dbContext)
        {
        }
        protected override IQueryable<PersonNotification> IncludeNavProperties(DbSet<PersonNotification> NavProperty)
        {
            return _dbset
                .Include(x => x.Person)
                .Include(x => x.Notification);
        }
    }
}
