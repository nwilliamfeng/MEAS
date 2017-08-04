using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    [Serializable]
    public class MeasureTestDaoBase:DaoBase
    {
        public string Code { get; set; }

  
        public DateTime TestDate { get; set; }

        public string Tester { get; set; }
    }
}
