using System;
using System.Collections.Generic;


namespace MEAS
{
    public class Customer:Entity
    {
        public Customer()
        {
            this.Contacts = new List<CustomerContact>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactPhone { get; set; }

  

        public virtual ICollection<CustomerContact> Contacts { get;   set; }
    }
}
