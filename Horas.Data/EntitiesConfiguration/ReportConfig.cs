namespace Horas.Data.EntitiesConfiguration
{
    public class ReportConfig : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Reason).IsRequired(false).HasMaxLength(500);
            builder.HasQueryFilter(x => x.IsExist);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
