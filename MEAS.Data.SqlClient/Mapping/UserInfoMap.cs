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
         
            //table  
            ToTable("Users");
        }
    }
}
