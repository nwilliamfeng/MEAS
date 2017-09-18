using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public   class CustomerContact:Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + LastName; }
        }

        public bool Sex { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public virtual Customer Company { get; set; }
    }
}
