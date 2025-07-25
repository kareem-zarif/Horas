﻿namespace Horas.Data.EntitiesConfiguration
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(x => x.IsExist);

            builder.HasOne(x => x.Customer)
                .WithOne(x => x.Cart)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
