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

            HasRequired(x => x.Product)
                .WithOptional()
                .Map(x => x.MapKey("ProductId"))
                .WillCascadeOnDelete(false);


            //Property(x => x.SerialNumber).IsRequired()
            //    .HasColumnAnnotation(IndexAnnotation.AnnotationName,new IndexAnnotation(
            //new IndexAttribute("Idx_SerialNumber") {IsUnique = true, Order=1 }));       

            Property(x => x.SerialNumber).IsRequired();
             

            Property(x => x.ManufactureDate).HasColumnType("datetime2").HasColumnOrder(3).IsOptional();

            HasRequired(x => x.Owner)
                   .WithOptional()
                   .Map(x => x.MapKey("OwnerId"))
                   .WillCascadeOnDelete(false);


            Property(x => x.Timestamp)
                .IsRequired()
                .IsRowVersion();  
            Property(x => x.Timestamp).IsRequired().IsConcurrencyToken();
            ToTable("TorqueWrenchs");
        }
    }
}
