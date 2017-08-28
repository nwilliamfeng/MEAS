using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.MySql;
using System.Diagnostics;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class CategoryTableTest
    {
        [TestMethod]
        public void  TestAppendMeasure()
        {
            CategoryRepository cr = new Tests.CategoryRepository();
           var results= cr.LoadAll();
            foreach (var d in results)
                d.Dump();
        }

        

      
       
    }
}
