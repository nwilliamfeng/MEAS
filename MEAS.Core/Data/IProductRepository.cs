using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> LoadAll();

        Task<IEnumerable<Product>> FindWithCategory(string category);

        Task<bool> Delete(int id);


        Task<Product> FindWithId(int productId);
    }
}
