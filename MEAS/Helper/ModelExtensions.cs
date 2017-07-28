using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MEAS.Models
{
    public static class ModelExtensions
    {
        public static SelectList ToSelectList(this IPagingInfo pageInfo)
        {
            List<int> pages = new List<int>();
            for (int i = 0; i < pageInfo.TotalPage; i++)
                pages.Add(i + 1);
            return new SelectList(pages);
        }
    }
}