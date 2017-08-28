using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEAS.Data;

namespace MEAS.Service
{
    public interface ITorqueWrenchMeasureService
    {
        Task<SearchResult<TorqueWrenchMeasure>> FindWithCode(string code,int pagesize=3,int pageIdx=0);

        Task<bool> Delete(int id);

        Task<TorqueWrenchMeasure> FindWithId(int id);

        Task<SearchResult<TorqueWrenchMeasure>> Find(DateTime start, DateTime end, int pagesize = 3, int pageIdx = 0);
    }
}
