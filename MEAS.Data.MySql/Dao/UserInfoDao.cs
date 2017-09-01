using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public sealed class UserInfoDao
    {
        public int Id { get; set; }


        public string LoginName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

      
    }
}
