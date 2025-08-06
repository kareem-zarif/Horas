namespace Horas.Data.EntitiesConfiguration
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(x => x.IsExist);

            builder.HasOne(x=>x.Order)
                .WithMany(x=>x.OrderItems)
                .HasForeignKey(x=>x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
