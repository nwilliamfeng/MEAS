using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEAS.Data
{
    public class UserProfileMap : EntityTypeConfiguration<UserProfileDao>
    {
        public UserProfileMap()
        {
         
            //table  
            ToTable("UserProfiles");
        }
    }
}
