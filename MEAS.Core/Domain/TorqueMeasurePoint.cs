using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public sealed class TorqueMeasurePoint
    {
        public TorqueMeasurePoint()
        {
            this.Values = new List<double>();
        }


        public double Nominal{ get; set; }

        public IList<double> Values { get;  set; }

       
    }
}
