using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public class TorqueWrenchMeasureSetting : Entity
    {
        public TorqueWrenchMeasureSetting()
        {
            this.NominalValues = new List<double>();
        }

        public int TestCount { get; set; }

        public ICollection<double> NominalValues { get;  set; }

     
        
    }
}
