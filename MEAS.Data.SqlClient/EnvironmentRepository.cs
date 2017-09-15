using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.SqlClient
{
    public class EnvironmentRepository :RepositoryBase<Environment>,  IEnvironmentRepository
    {
        //public async Task<bool> Add(Environment environment)
        //{

        //    using (var db = new SqlServerDbContext())
        //    {
        //        var ev= db.Environments.Add(environment);
        //        var count = await db.SaveChangesAsync();
        //        if (count > 0)
        //        {
        //            environment.Id = ev.Id;
        //            environment.Timestamp = ev.Timestamp;
        //        }
             
        //        return count == 1;
        //    }
        //}

        //public async Task<Environment> Find(int id)
        //{
        //    using (var db = new SqlServerDbContext())
        //    {
        //        return await db.Environments.FindAsync(id); 
        //    }
        //}

        public Task<SearchResult<Environment>> Find(DateTime start, DateTime end, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    var count = db.Environments.Where(x => x.Time >= start && x.Time <= end).Select(x => x.Id).Count();
                    var data = db.Environments.Where(x => x.Time >= start && x.Time <= end)
                        .OrderByDescending(x => x.Time)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<Environment>(data, count);
                }
            });
        }

        //public async Task<bool> Remove(int id)
        //{
        //    using (var db = new SqlServerDbContext())
        //    {
        //        Environment ev = new Environment { Id = id };
        //        db.Environments.Attach(ev);
        //        db.Environments.Remove(ev);
        //        var count = await db.SaveChangesAsync();
        //        return count == 1;
        //    }
        //}

        //public async Task<bool> Update(Environment environment)
        //{
        //    using (var db = new SqlServerDbContext())
        //    {                
        //        var ev= db.Environments.Attach(environment);
        //        DbEntityEntry<Environment> entry = db.Entry(environment);

        //        db.ObjectStateManager().ChangeObjectState(environment, EntityState.Modified);
        //        var count = await db.SaveChangesAsync();
        //        if (count > 0)
        //            environment.Timestamp = ev.Timestamp;
        //        return count == 1;
        //    }
        //}
    }
}
