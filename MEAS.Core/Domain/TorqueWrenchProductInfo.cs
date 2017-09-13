using System;
using System.Collections.Generic;
 

namespace MEAS
{
    public sealed class TorqueWrenchProductInfo:Entity
    {
        public string Type { get; set; }

        public string Manufacturer { get; set; }

        public string Name { get; set; }

        public double MinRange { get; set; }

        public double MaxRange { get; set; }
        
        public WorkDirection WorkDirection { get; set; }
 

    }
}
