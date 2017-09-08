using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEAS.Data
{
    public class TorqueWrenchMeasureMap : EntityTypeConfiguration<TorqueWrenchMeasureDao>
    {
        public TorqueWrenchMeasureMap()
        {
            HasKey(x => x.Id); //主键
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                .HasColumnOrder(1);

            Property(x => x.TestCode)
                .HasMaxLength(10)
                .HasColumnOrder(2)
                .HasColumnAnnotation("Idx_TestCode", new IndexAnnotation(new IndexAttribute { IsUnique=true}));

            Property(x => x.TestDate).HasColumnType("datetime2").HasColumnOrder(3);

            //https://stackoverflow.com/questions/5421707/ef-4-1-difference-between-withmany-and-withoptional?rq=1
              HasRequired(x => x.Tester)
                .WithOptional()
                .Map(x => x.MapKey("TeserId"))
                .WillCascadeOnDelete(false);
         
            
            
            Property(x => x.Timestamp)
                .IsRequired()
                .IsRowVersion(); //时间戳可以用rowversion或者IsConcurrencyToken
            // Property(x => x.Timestamp).IsRequired().IsConcurrencyToken();
            ToTable("TorqueWrenchMeasures");
        }
    }
}
