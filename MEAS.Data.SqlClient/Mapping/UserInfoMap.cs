using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEAS.Data
{
    public class UserInfoMap : EntityTypeConfiguration<UserInfoDao>
    {
        public UserInfoMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnOrder(1).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.LoginName).HasColumnOrder(2).IsRequired().HasMaxLength(12);
            Property(x => x.UserName).HasColumnOrder(3).IsRequired().HasMaxLength(12);
            Property(x => x.Password).HasColumnOrder(4).IsRequired().HasMaxLength(15);
         
            //table  
            ToTable("Users");
        }
    }
}
