using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEAS.Data
{
    public class EnvironmentMap : EntityTypeConfiguration<Environment>
    {
        public EnvironmentMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnOrder(1).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Time).HasColumnOrder(2).IsRequired().HasColumnType("datetime2");
            Property(x => x.Humidity).HasColumnOrder(3).IsRequired();

            Property(x => x.Temperature).HasColumnOrder(4).IsRequired();
            Property(x => x.Address).HasColumnOrder(5).IsRequired();
            Property(x => x.Timestamp)
                .HasColumnOrder(6)
                .IsRequired()
                .IsRowVersion(); //时间戳可以用rowversion或者IsConcurrencyToken
            ToTable("Environments");
        }
    }
}
