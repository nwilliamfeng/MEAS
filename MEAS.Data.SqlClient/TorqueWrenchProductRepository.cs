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
                    var count = db.TorqueWrenchProducts.Where(x => x.Model.Contains(model)).Select(x => x.Id).Count();
                    var data = db.TorqueWrenchProducts.Where(x => x.Model.Contains(model))
                        .OrderByDescending(x => x.Id)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<TorqueWrenchProduct>(data, count);
                }
            });
        }

   

        public Task<SearchResult<TorqueWrenchProduct>> FindWithRange(double min, double max, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    var count = db.TorqueWrenchProducts.Where(x => x.MinRange >=min && x.MaxRange<=max).Select(x => x.Id).Count();
                    var data = db.TorqueWrenchProducts.Where(x => x.MinRange >= min && x.MaxRange <= max)
                        .OrderByDescending(x => x.Id)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<TorqueWrenchProduct>(data, count);
                }
            });
        }
    }
}
