using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.SqlClient
{
    public class TorqueWrenchProductRepository :RepositoryBase<TorqueWrenchProduct>, ITorqueWrenchProductRepository
    {
      
        public Task<SearchResult<TorqueWrenchProduct>> FindWithModel(string model, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    var count = db..Where(x => x.FullName.Contains(model)).Select(x => x.Id).Count();
                    var data = db.CustomerContacts.Where(x => x.FullName.Contains(model))
                        .OrderByDescending(x => x.Id)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<CustomerContact>(data, count);
                }
            });
        }

   

        public Task<SearchResult<TorqueWrenchProduct>> FindWithRange(double min, double max, int pageSize = 5, int pageNumber = 0)
        {
            throw new NotImplementedException();
        }
    }
}
