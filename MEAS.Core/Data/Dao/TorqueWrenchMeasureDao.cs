using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    [Serializable]
    public class TorqueWrenchMeasureDao : MeasureDaoBase
    {
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", this.Id, this.Code, this.TestDate);
        }
    }
}
