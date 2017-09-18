using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface ITorqueWrenchMeasureRepository
    {

        Task<bool> Add(TorqueWrenchMeasure measure);

        Task<SearchResult<TorqueWrenchMeasure>> FindWithCode(string code,int pagesize=3,int pageidx=0);

        Task<bool> Remove(int id);

        Task<TorqueWrenchMeasure> Find(int id);

        Task<bool> Update(TorqueWrenchMeasure measure);

        Task<SearchResult<TorqueWrenchMeasure>> Find(DateTime start, DateTime end, int pagesize = 3, int pageidx = 0);
    }
}
