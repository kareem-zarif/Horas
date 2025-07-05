namespace Horas.Data.EntitiesConfiguration
{
    public class WishlistConfig : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(x => x.IsExist);
        }
    }
}
