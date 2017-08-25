using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAS.Data;

namespace MEAS.Tests
{
    public static class Extension
    {
        public static void Dump(this TorqueWrenchMeasureDao dao)
        {
            Console.WriteLine(dao.Id +"   "+dao.TestCode +"   "+dao.TestDate);
            if (dao.Tester != null)
            {
                Console.WriteLine(dao.Tester.Id+"  "+dao.Tester.LoginName+"  "+dao.Tester.Password ); 
            }
        }
    }
}
