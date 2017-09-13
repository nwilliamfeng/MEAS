using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    [Serializable]
    public class Product:Entity
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Manufacturer { get; set; }

        public string Category { get; set; }
    }
}
