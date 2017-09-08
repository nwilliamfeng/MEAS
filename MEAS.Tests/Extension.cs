using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAS.Data;
using System.Reflection;

namespace MEAS.Tests
{
    public static class Extension
    {
        public static void Dump(this TorqueWrenchMeasure dao)
        {
            Console.WriteLine(dao.Id +"   "+dao.TestCode +"   "+dao.TestDate);
            if (dao.Tester != null)
            {
                Console.WriteLine(dao.Tester.Id+"  "+dao.Tester.LoginName+"  "+dao.Tester.Password ); 
            }
        }

        public static void Dump(this UserInfo user)
        {
            Console.WriteLine("********************user info ********************************"); 
            user.GetType().GetProperties().ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name+":"+x.GetValue(user));
            });
            Console.WriteLine("********************end********************************");
        }
    }
}
