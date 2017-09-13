using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEAS.Data
{
    [Table("Users")]
    public sealed class UserInfoDao :DaoBase
    {
       
        public string LoginName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

        public byte[] Avatar { get; set; }

        public string Phone { get; set; }

        [StringLength(11,MinimumLength =11)]
        public string Mobile { get; set; }

        
        public string EmailAddress { get; set; }

    }
}
