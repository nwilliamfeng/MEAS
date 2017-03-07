using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public class ManufacturerService : IManufacturerService
    {
        public Manufacturer Find(string name)
        {
            return new Manufacturer { Name=name};
        }
    }
}
