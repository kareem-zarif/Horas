namespace Horas.Data.EntitiesConfiguration
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            builder.HasQueryFilter(x => x.IsExist);
        }
    }
}
