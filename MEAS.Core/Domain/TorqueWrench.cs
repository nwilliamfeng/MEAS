using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public class TorqueWrench:Entity
    {
        public TorqueWrenchProduct Product { get; set; }
       
        public string SerialNumber { get; set; }

        public DateTime ManufactureDate { get; set; }

        public CustomerContact Owner { get; set; }
 
    }
}
