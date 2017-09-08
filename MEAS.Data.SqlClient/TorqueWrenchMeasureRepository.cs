using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MEAS.Data.SqlClient
{
    public class TorqueWrenchMeasureRepository :ITorqueWrenchMeasureRepository
    {   
     
        public async Task<bool> Delete(int id)
        {
            return false;
        }

        public Task<SearchResult<TorqueWrenchMeasure>> FindWithCode(string code, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {
               
                    return   SearchResult<TorqueWrenchMeasure>.Empty;
            
            });          
        }

        public  Task<TorqueWrenchMeasure> FindWithId(int id)
        {
            return Task.Run(()=>
            {
                return new TorqueWrenchMeasure { };
            });
        }

        public Task<SearchResult<TorqueWrenchMeasure>> Find(DateTime start, DateTime end, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {
                return SearchResult<TorqueWrenchMeasure>.Empty;
            });
        }

        public async Task<bool> Add(TorqueWrenchMeasure measure)
        {
            return false;
        }

        public async Task<bool> Update(TorqueWrenchMeasure measure)
        {
            return false;
        }
    }
}
