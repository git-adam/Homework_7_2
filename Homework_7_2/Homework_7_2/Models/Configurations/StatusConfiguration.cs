using Homework_7_2.Models.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Homework_7_2.Models.Configurations
{
    public class StatusConfiguration : EntityTypeConfiguration<Status>
    {
        public StatusConfiguration()
        {
            ToTable("dbo.Statuses");

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
