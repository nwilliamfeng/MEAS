using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface ITorqueWrenchProductRepository
    {
        Task<bool> Add(TorqueWrenchProduct product);

        Task<bool> Update(TorqueWrenchProduct product);

        Task<bool> Remove(int id);

        Task<SearchResult<TorqueWrenchProduct>> FindWithModel(string model,int pageSize=5,int pageNumber=0);

        Task<SearchResult<TorqueWrenchProduct>> FindWithRange(double min,double max, int pageSize = 5, int pageNumber = 0);

        Task<TorqueWrenchProduct> Find(int id);
    }
}
