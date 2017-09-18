using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    [Serializable]
    public class TorqueWrenchMeasure : MeasureBase
    {
        public TorqueWrenchMeasureSetting Setting { get; set; }
    }
}
