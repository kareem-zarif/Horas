namespace Horas.Data.EntitiesConfiguration
{
    public class OrderStatusHistoryConfig : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(x => x.IsExist);
        }
    }
}
