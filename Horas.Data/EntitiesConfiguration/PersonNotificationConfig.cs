using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horas.Data.EntitiesConfiguration
{
    public class PersonNotificationConfig : IEntityTypeConfiguration<PersonNotification>
    {
        public void Configure(EntityTypeBuilder<PersonNotification> builder)
        {
            builder.HasKey(x => x.Id); // Keep Id from IBaseEnt for auditing
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => new { x.PersonId, x.NotificationId }).IsUnique();
            builder.HasQueryFilter(x => x.IsExist);

            builder.HasOne(x => x.Person)
              .WithMany(x => x.PersonNotifications)
              .HasForeignKey(x => x.PersonId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Notification)
                .WithMany(x => x.PersonNotifications)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
