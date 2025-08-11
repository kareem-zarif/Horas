namespace Horas.Data.EntitiesConfiguration { 
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Street).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.City).HasMaxLength(50).IsRequired();
            builder.Property(x => x.State).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Country).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.PostalCode).HasMaxLength(50).IsRequired(false);
            builder.HasQueryFilter(x => x.IsExist);
        }
    }
}