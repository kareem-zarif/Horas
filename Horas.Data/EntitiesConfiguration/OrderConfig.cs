namespace Horas.Data.EntitiesConfiguration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(x => x.IsExist);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.PaymentMethod)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x=>x.OrderItems)
                .WithOne(x=>x.Order)
                .HasForeignKey(x=> x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
