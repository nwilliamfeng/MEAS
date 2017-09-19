using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public sealed class TorqueWrenchMeasureSetting 
    {
        public TorqueWrenchMeasureSetting()
        {
            this.NominalValues = new List<double>();
        }

        public int TestCount { get; set; }

        public IList<double> NominalValues { get;  set; }
         
    }
}
