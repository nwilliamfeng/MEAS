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
    public class TorqueWrenchMap : EntityTypeConfiguration<TorqueWrench>
    {
        public TorqueWrenchMap()
        {
            HasKey(x => x.Id); //主键
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                .HasColumnOrder(1);

            Property(x => x.TestCode)
                .HasColumnAnnotation("Idx_TestCode", new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            //   Property(x => x.TestDate).HasColumnType("datetime2").HasColumnOrder(3);

         

            HasRequired(x => x.Product)
                .WithOptional()
                .Map(x => x.MapKey("ProductId"))
                .WillCascadeOnDelete(false);

            Property(x => x.Checker).IsOptional();

            // HasOptional(x => x.Checker) //此处用optional是为了新建实例时Checker无需赋值，所以对应的表字段应该是nullable 
            //    .WithOptionalDependent()    
            //.Map(x => x.MapKey("CheckerId"))
            //.WillCascadeOnDelete(false);

 

            //Property(x => x.Timestamp)
            //    .IsRequired()
            //    .IsRowVersion(); //时间戳可以用rowversion或者IsConcurrencyToken
            // Property(x => x.Timestamp).IsRequired().IsConcurrencyToken();
            //ToTable("TorqueWrenchMeasures");
        }
    }
}
