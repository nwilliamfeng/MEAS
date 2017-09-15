using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface ITorqueWrenchRepository
    {
        Task<bool> Add(TorqueWrench wrench);

        Task<bool> Update(TorqueWrench wrench);

        Task<bool> Remove(int id);

        Task<SearchResult<TorqueWrench>> FindWithModel(string model,int pageSize=5,int pageNumber=0);

        Task<SearchResult<TorqueWrench>> FindWithSerailNumber(string sn, int pageSize = 5, int pageNumber = 0);

        Task<SearchResult<TorqueWrench>> FindWithRange(double min,double max, int pageSize = 5, int pageNumber = 0);

        Task<TorqueWrenchProduct> Find(int id);
    }
}
