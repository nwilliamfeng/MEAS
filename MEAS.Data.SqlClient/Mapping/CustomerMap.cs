using System.Data.Entity.ModelConfiguration;


namespace MEAS.Data
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnOrder(1).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnOrder(2).IsRequired();
            Property(x => x.Address).HasColumnOrder(3).IsRequired();
            Property(x => x.ContactPhone).HasColumnOrder(4).HasColumnOrder(4);

            HasMany(x => x.Contacts).WithRequired(x => x.Company).WillCascadeOnDelete(true);
           
            Property(x => x.Timestamp) .HasColumnOrder(8) .IsRequired().IsRowVersion(); //时间戳可以用rowversion或者IsConcurrencyToken
            ToTable("Customers");
        }
    }
}
