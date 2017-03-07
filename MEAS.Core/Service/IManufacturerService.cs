using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public interface IManufacturerService
    {
        Manufacturer Find(string name);
    }
}
