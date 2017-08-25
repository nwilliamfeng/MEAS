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
        public string Code { get; set; }

  
        public DateTime MeasureTime { get; set; }

        public UserInfo User { get; set; }
    }
}
