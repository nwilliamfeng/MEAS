using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEAS.Data
{
    public class TorqueWrenchProductMap : EntityTypeConfiguration<TorqueWrenchProduct>
    {
        public TorqueWrenchProductMap()
        {
            HasKey(x => x.Id);          
            Property(x => x.Id).HasColumnOrder(1).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnOrder(2).IsOptional() ;
            Property(x => x.Model).HasColumnOrder(3).IsRequired();
            Property(x => x.Manufacturer).HasColumnOrder(4).IsRequired();
            Property(x => x.MinRange).HasColumnOrder(5).IsRequired();
            Property(x => x.MaxRange).HasColumnOrder(6).IsRequired();
            Property(x => x.WorkDirection).HasColumnOrder(7).IsRequired();
      
            Property(x => x.Timestamp)
                .HasColumnOrder(8)
                .IsRequired()
                .IsRowVersion(); //时间戳可以用rowversion或者IsConcurrencyToken
            ToTable("TorqueWrenchProducts");
        }
    }
}
