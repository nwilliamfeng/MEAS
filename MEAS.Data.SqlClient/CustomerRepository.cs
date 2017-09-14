using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.SqlClient
{
    public class CustomerRepository :RepositoryBase<Customer>,  ICustomerRepository
    {
        public override async Task<bool> Add(Customer customer)
        {

            using (var db = new SqlServerDbContext())
            {
                var ev= db.Customers.Add(customer);
                var count = await db.SaveChangesAsync();
                if (count > 0)
                {
                    customer.Id = ev.Id;
                    customer.Timestamp = ev.Timestamp;
                }
             
                return count == 1;
            }
        }

        public override async Task<Customer> Find(int id)
        {
            using (var db = new SqlServerDbContext())
            {
                return await db.Customers.Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public Task<SearchResult<Customer>> Find(string name, int pageSize = 5, int pageNumber = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    var count = db.Customers.Where(x => x.Name.Contains(name) ).Select(x => x.Id).Count();
                    var data = db.Customers.Where(x => x.Name.Contains(name))
                        .OrderByDescending(x => x.Id)
                       .Skip(pageSize * pageNumber)
                       .Take(pageSize).ToList();
                    return new SearchResult<Customer>(data, count);
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

        public override async Task<bool> Update(Customer  customer)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var original = dc.Customers.Include(x => x.Contacts).Single(x => x.Id == customer.Id);

                //删除不存在的contact
                original.Contacts.Where(x => !customer.Contacts.Contains(x)).ToList().ForEach(x =>
                  {
                      dc.CustomerContacts.Attach(x);
                      dc.Entry(x).State = EntityState.Deleted;
                  });

                customer.Contacts.Where(x => x.Id == 0).ToList()
                    .ForEach(x =>
                    {
                    });
                //if (!original.Contacts.Count<customer.c)  
                //{
                //    dc.Environments.Attach(curr.Environment); //如果不attach，则ef会让db自动添加一个实例
                //    original.Environment = curr.Environment;
                //}
        
                dc.Entry(original).CurrentValues.SetValues(customer);
                var result = await dc.SaveChangesAsync();
                return result > 0;
            }
        }
    }
}
