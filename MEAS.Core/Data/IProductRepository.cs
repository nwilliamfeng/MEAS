using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface IProductRepository
    {
         

        Task<IEnumerable<Product>> FindWithCategory(string category);


        Task<Product> FindWithId(int productId);
    }
}
