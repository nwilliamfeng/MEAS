using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public class Page:Entity
    {
       
    //    public string RelativeUrl { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }
       

        public string Title { get; set; }

        public PageCategory Category { get; set; }
    }
}
