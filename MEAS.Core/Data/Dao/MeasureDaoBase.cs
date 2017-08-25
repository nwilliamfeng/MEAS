using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    [Serializable]
    public class MeasureDaoBase:DaoBase
    {
        public string TestCode { get; set; }

  
        public DateTime TestDate { get; set; }

        public UserInfoDao Tester { get; set; }
    }
}
