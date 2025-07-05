namespace Horas.Data.EntitiesConfiguration
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Message).HasMaxLength(1000).IsRequired();
            builder.HasQueryFilter(x => x.IsExist);

        }
    }
}
