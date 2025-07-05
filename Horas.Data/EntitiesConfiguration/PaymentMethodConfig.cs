namespace Horas.Data.EntitiesConfiguration
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.PaymentType).IsRequired();
            builder.Property(x => x.CardNumber).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.CardHolderName).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.ExpireDate).IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.FawryCode).HasMaxLength(50).IsRequired(false);
            builder.HasQueryFilter(x => x.IsExist);
        }
    }
}
