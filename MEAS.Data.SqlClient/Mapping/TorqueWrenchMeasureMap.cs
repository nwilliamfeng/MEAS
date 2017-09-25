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
            //HasKey(x => x.Id); //主键
            //Property(x => x.Id)
            //    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
            //    .HasColumnOrder(1);

            Property(x => x.TestCode)
                .HasColumnAnnotation( IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("Idx_TestCode") { IsUnique = true }));

            //   Property(x => x.TestDate).HasColumnType("datetime2").HasColumnOrder(3);

            //https://stackoverflow.com/questions/5421707/ef-4-1-difference-between-withmany-and-withoptional?rq=1
            //HasRequired(x => x.Tester)
            //    .WithOptional()
            //    .Map(x => x.MapKey("TeserId"))
            //    .WillCascadeOnDelete(false);

            HasRequired(x => x.Measurand)
              .WithOptional()
              .Map(x => x.MapKey("MeasurandId"))
              .WillCascadeOnDelete(false);


            HasRequired(x => x.Environment)
                .WithOptional()
                .Map(x => x.MapKey("EnvironmentId"))
                .WillCascadeOnDelete(false);


            HasRequired(x => x.Standard)
            .WithOptional()
            .Map(x => x.MapKey("StandardId"))
            .WillCascadeOnDelete(false);

            //HasRequired(x => x.Setting)
            //.WithOptional()
            //.Map(x => x.MapKey("SettingId"))
            //.WillCascadeOnDelete(false);

            Property(x => x.Checker).IsOptional();

            Property(x => x.AcceptTime).IsOptional();


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
