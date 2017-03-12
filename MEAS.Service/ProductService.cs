using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public class ProductService : IProductService
    {
        public IEnumerable<Product> Find(string model)
        {
            yield return new Product { Model = model + "1", Name = "ProductA", Price = 100.2m };
            yield return new Product { Model = model + "2", Name = "ProductB", Price = 30.6m };
            yield return new Product { Model = model + "3", Name = "ProductC", Price = 34.9m };
        }
    }
}
