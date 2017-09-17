using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.SqlClient
{
    public class TorqueWrenchRepository :RepositoryBase<TorqueWrench>, ITorqueWrenchRepository
    {
      
        public Task<SearchResult<TorqueWrench>> FindWithModel(string model, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    var count = db.TorqueWrenchs.Include(x => x.Product).Where(x => x.Product.Model.Contains(model)).Select(x => x.Id).Count();
                    var data = db.TorqueWrenchs
                    .Include(x => x.Product)
                    .Include(x=>x.Owner)
                    .Where(x => x.Product.Model.Contains(model))
                        .OrderByDescending(x => x.Id)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<TorqueWrench>(data, count);
                }
            });
        }



        public Task<SearchResult<TorqueWrench>> FindWithRange(double min, double max, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var count = db.TorqueWrenchs.Include(x => x.Product).Where(x => x.Product.MinRange <= min && x.Product.MaxRange >= max).Select(x => x.Id).Count();
                    var data = db.TorqueWrenchs
                    .Include(x => x.Product)
                    .Where(x => x.Product.MinRange <= min && x.Product.MaxRange >= max)
                    .Include(x => x.Owner)
                    .OrderByDescending(x => x.Id)
                    .Skip(pageSize * pageNumber)
                     .Take(pageSize).ToList();
                    return new SearchResult<TorqueWrench>(data, count);
                }
            });
        }

        public Task<SearchResult<TorqueWrench>> FindWithSerialNumber(string sn, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var count = db.TorqueWrenchs.Where(x => x.SerialNumber.Contains(sn)).Select(x => x.Id).Count();
                    var data = db.TorqueWrenchs
                    .Include(x=>x.Product)
                    .Include(x=>x.Owner)
                    .Where(x => x.SerialNumber.Contains(sn))
                        .OrderByDescending(x => x.Id)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<TorqueWrench>(data, count);
                }
            });
        }

        public override async Task<bool> Remove(int id)
        {
            using (var db = new SqlServerDbContext())
            {
                var wrench = db.TorqueWrenchs.Find(id);
                 db.TorqueWrenchs.Attach(wrench);

                db.TorqueWrenchs.Remove(wrench);
                var count = await db.SaveChangesAsync();
                return count > 0;  
            }
        }

        public override Task<TorqueWrench> Find(int id)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.TorqueWrenchs
                     .Include(x => x.Product)
                    .Include(x => x.Owner)
                    .FirstOrDefault(x => x.Id == id);

                }
            });
        }

        public async override Task<bool> Update(TorqueWrench source)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var original = dc.TorqueWrenchs.Include(x => x.Owner).Include(x=>x.Product).Single(x => x.Id == source.Id);
      
                if (!original.Owner.Equals(source.Owner))  
                {
                    dc.Customers.Attach(source.Owner);  
                    original.Owner = source.Owner;
                }
                if (!original.Product.Equals(source.Product))  
                {
                    dc.TorqueWrenchProducts.Attach(source.Product);  
                    original.Product = source.Product;
                }

                var entry = dc.Entry(original);
                entry.CurrentValues.SetValues(source);
                var result = await dc.SaveChangesAsync();
                if (result > 0)
                {
                    AutoMapper.Mapper.Map(original, source);
                }
                return result > 0;
            }
        }

    }
}
