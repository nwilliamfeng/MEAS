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

        public async Task<bool> Delete(int id)
        {
            return await this._rp.Delete(id);
        }

        public async Task<IEnumerable<Product>> FindWithCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return await this._rp.LoadAll();
            return await this._rp.FindWithCategory(category);
        }

        public async Task<Product> FindWithId(int id)
        {
            return await this._rp.FindWithId(id);
        }
    }
}
