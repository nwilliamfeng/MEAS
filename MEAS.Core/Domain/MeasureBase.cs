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

  
     

        public string Tester { get; set; }

        public string Checker { get; set; }

        public Environment Environment { get; set; }


    }
}
