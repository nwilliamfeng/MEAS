using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface IEnvironmentRepository
    {
        Task<bool> Add(Environment environment);

        Task<bool> Update(Environment environment);

        Task<bool> Remove(int id);

        Task<SearchResult<Environment>> Find(DateTime start, DateTime end,int pageSize=5,int pageNumber=0);

        Task<Environment> Find(int id);
    }
}
