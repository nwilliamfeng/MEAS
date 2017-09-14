using System.Data.Entity.ModelConfiguration;


namespace MEAS.Data
{
    public class CustomerContactMap : EntityTypeConfiguration<CustomerContact>
    {
        public CustomerContactMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnOrder(1).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.FirstName).HasColumnOrder(2).IsRequired();
            Property(x => x.LastName).HasColumnOrder(3).IsRequired();
            Property(x => x.Sex).HasColumnOrder(4);
            Property(x => x.Phone).HasColumnOrder(5);
            Property(x => x.Mobile).HasColumnOrder(6);
            HasRequired(x => x.Company).WithMany(x => x.Contacts).Map(x=>x.MapKey("CustomerId"));
            Property(x => x.Timestamp) .HasColumnOrder(8) .IsRequired().IsRowVersion(); //时间戳可以用rowversion或者IsConcurrencyToken
            ToTable("CustomerContacts");
        }
    }
}
