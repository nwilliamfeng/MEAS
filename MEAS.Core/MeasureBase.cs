using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    [Serializable]
    public class MeasureBase:Entity
    {
        public string TestCode { get; set; }

  
        public DateTime TestDate { get; set; }

        public UserInfo Tester { get; set; }
    }
}
