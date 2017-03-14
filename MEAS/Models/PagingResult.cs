using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEAS.Models
{
    public class PagingResult<T>:IPagingInfo
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

       

        public int TotalPage
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public IEnumerable<T> Values { get; set; }
    }
}