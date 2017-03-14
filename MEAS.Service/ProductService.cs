using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAS.Data;

namespace MEAS.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository _rp;
       public ProductService(IProductRepository rp)
        {
            this._rp = rp;
        }

        public async Task<IEnumerable<Product>> FindWithCategory(string category)
        {
            return await this._rp.FindWithCategory(category);
        }
    }
}
