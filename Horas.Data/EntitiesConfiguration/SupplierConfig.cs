namespace Horas.Data.EntitiesConfiguration
{
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(x => x.FactoryName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.IFactoryPicPath).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.BankAccountName).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.BankAccountNumber).IsRequired(false).HasMaxLength(50);
        }
    }
}
