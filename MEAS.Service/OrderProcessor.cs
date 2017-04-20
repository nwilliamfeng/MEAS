using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public class OrderProcessor : IOrderProcessor
    {
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            var lines = cart.Lines.ToList();
            Console.WriteLine(string.Format("Process the order : {0}",DateTime.Now ));
            Console.WriteLine(string.Format("the items : {0}",lines));
            for(int i=0;i<cart.Lines.Count();i++)
            {
                Console.WriteLine(string.Format("the item {0} :  product {1}",i+1,lines[i].Product.Name));
            }
            Console.WriteLine("finish"); 
        }
    }
}
