using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.SqlClient
{
    public class CustomerContactRepository :RepositoryBase<CustomerContact>, ICustomerContactRepository
    {
        //public async Task<bool> Add(CustomerContact contact)
        //{

        //    using (var db = new SqlServerDbContext())
        //    {
        //        var ev= db.CustomerContacts.Add(contact);
        //        var count = await db.SaveChangesAsync();
        //        if (count > 0)
        //        {
        //            contact.Id = ev.Id;
        //            contact.Timestamp = ev.Timestamp;
        //        }

        //        return count == 1;
        //    }
        //}

        //public async Task<CustomerContact> Find(int id)
        //{
        //    using (var db = new SqlServerDbContext())
        //    {
        //        return await db.CustomerContacts.FindAsync(id); 
        //    }
        //}

        public Task<SearchResult<CustomerContact>> Find(string name, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    var count = db.CustomerContacts.Where(x => x.FullName.Contains(name)).Select(x => x.Id).Count();
                    var data = db.CustomerContacts.Where(x => x.FullName.Contains(name))
                        .OrderByDescending(x => x.Id)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<CustomerContact>(data, count);
                }
            });
        }

        //public async Task<bool> Remove(int id)
        //{
        //    using (var db = new SqlServerDbContext())
        //    {
        //        conta ev = new Environment { Id = id };
        //        db.Environments.Attach(ev);
        //        db.Environments.Remove(ev);
        //        var count = await db.SaveChangesAsync();
        //        return count == 1;
        //    }
        //}

        //public async Task<bool> Update(CustomerContact contact)
        //{
        //    using (var db = new SqlServerDbContext())
        //    {                
        //        var ev= db.CustomerContacts.Attach(contact);
        //        DbEntityEntry<CustomerContact> entry = db.Entry(contact);

        //        db.ObjectStateManager().ChangeObjectState(contact, EntityState.Modified);
        //        var count = await db.SaveChangesAsync();
        //        if (count > 0)
        //            contact.Timestamp = ev.Timestamp;
        //        return count >0;
        //    }
        //}
    }
}
