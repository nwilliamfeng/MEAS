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
            Console.WriteLine(string.Format("**************************** start dump {0} ********************************", obj.GetType().Name));
            obj.GetType().GetProperties().ToList().ForEach(x =>
            {
                var ov = x.GetValue(obj);
                if (ov is Entity)
                    ov.Dump();
                else if(ov is byte[])
                {
                    Console.WriteLine(x.Name + ":" + (ov as byte[]).DumpTimestamp());
                }
                else
                    Console.WriteLine(x.Name + ":" +ov);
            });
            Console.WriteLine(string.Format("******************** end dump {0} ******************************** ",obj.GetType().Name));
            Console.WriteLine();
            Console.WriteLine();
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
