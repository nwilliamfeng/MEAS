using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEAS
{
    public class PageCategory:Entity
    {
        public PageCategory()
        {
            this.Pages = new List<Page>();
        }

        public virtual ICollection<Page> Pages { get; set; }


        public string Name { get; set; }

        public string Title { get; set; }
    
    }
}