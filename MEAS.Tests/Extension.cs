using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAS.Data;
using System.Reflection;
using System.Collections;

namespace MEAS.Tests
{
    public static class Extension
    {
        
        public static void Dump(this object obj)
        {
            Console.WriteLine(string.Format("******************** {0} ********************************",obj.GetType().Name));
            obj.GetType().GetProperties().ToList().ForEach(x =>
            {
                var ov = x.GetValue(obj);
                if (ov is Entity)
                    ov.Dump();
             
                else
                    Console.WriteLine(x.Name + ":" +ov);
            });
            Console.WriteLine(string.Format("******************** {0} ********************************",obj.GetType().Name));
        }


        public static string DumpTimestamp(this byte[] timestamp)
        {
            string s = null;
            foreach (var t in timestamp)
                s += t.ToString();
            return s;
        }
    }
}
