using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public class TorqueStandard:Entity
    {
        public string Name { get; set; }

        public string CertificateName { get; set; }

        public double MinRange { get; set; }

        public double MaxRange { get; set; }

        public string Model { get; set; }
    }
}
