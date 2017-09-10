using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEAS.Data
{
    [Serializable]
    public class MeasureDaoBase:DaoBase
    {
        [Index(IsUnique =true)]
        public string TestCode { get; set; }

  
        public DateTime TestDate { get; set; }

        public UserInfoDao Tester { get; set; }

        
    }
}
