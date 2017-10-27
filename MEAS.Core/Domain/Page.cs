using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public class Page:Entity
    {
     
        public string Action { get; set; }

        public string Controller { get; set; }

        public string RedirectActionName { get; set; }

      
        public string Title { get; set; }

        public PageCategory Category { get; set; }
    }
}
