

using Horas.Domain.Entities;

namespace Horas.Data.EntitiesConfiguration
{
    public class ProductWishlistConfig : IEntityTypeConfiguration<ProductWishList>
    {
        public void Configure(EntityTypeBuilder<ProductWishList> builder)
        {
            builder.HasKey(x => x.Id); // Keep Id from IBaseEnt for auditing
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => new { x.ProductId, x.WishListId }).IsUnique();
            builder.HasQueryFilter(x => x.IsExist);


            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductWishLists)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WishList)
                .WithMany(x => x.ProductWishLists)
                .HasForeignKey(x => x.WishListId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
   
}
