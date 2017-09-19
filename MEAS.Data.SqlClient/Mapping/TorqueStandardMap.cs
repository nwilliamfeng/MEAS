using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEAS.Data
{
    public class TorqueStandardMap : EntityTypeConfiguration<TorqueStandard>
    {
        public TorqueStandardMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnOrder(1).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
          
            Property(x => x.Name).HasColumnOrder(2).IsOptional();
            Property(x => x.Model).HasColumnOrder(3).IsOptional();
            Property(x => x.MinRange).HasColumnOrder(4);
            Property(x => x.MaxRange).HasColumnOrder(5);
            Property(x => x.CertificateName).HasColumnOrder(6).IsOptional();
            Property(x => x.Timestamp)
                .HasColumnOrder(12)
                .IsRequired()
                .IsRowVersion();  
            ToTable("TorqueStandards");
        }
    }
}
