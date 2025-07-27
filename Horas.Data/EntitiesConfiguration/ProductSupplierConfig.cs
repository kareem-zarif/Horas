


using Horas.Domain.Entities;

namespace Horas.Data.EntitiesConfiguration
{
    public class ProductSupplierConfig : IEntityTypeConfiguration<ProductSupplier>
    {
        public void Configure(EntityTypeBuilder<ProductSupplier> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.HasIndex(x => new { x.ProductId, x.SupplierId }).IsUnique();


        }
    }
}
