namespace Horas.Data.EntitiesConfiguration
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Body).HasMaxLength(100).IsRequired();
            builder.Property(x => x.SenderType).IsRequired().HasMaxLength(50);
            builder.HasQueryFilter(x => x.IsExist);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Messages)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.Messages)
                .OnDelete(DeleteBehavior.Restrict);
            //Cascade:: Customer delete cause all related messages
            //Restrict :: can not delete Customer till delete message(depency) 
            //NoAction :: Same Restrict => for manual delete logic
            //SetNull :: for save history of messages even after deletong Customer
        }
    }
}
