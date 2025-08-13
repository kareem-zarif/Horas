namespace Horas.Data.EntitiesConfiguration
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(x => x.IsExist);

            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Cart).HasForeignKey<Cart>(ca => ca.CustomerId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.CustomerId)
                   .IsUnique(); //backend بيرفض إنشاء كارت لنفس العميل مرتين:
        }
    }
}
