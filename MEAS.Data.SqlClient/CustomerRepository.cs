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
             
                return count >0;
            }
        }

        public override async Task<Customer> Find(int id)
        {
            using (var db = new SqlServerDbContext())
            {
                return await db.Customers.Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Id == id);
            }
        }


        public override async Task<bool> Remove(int id)
        {
            using (var db = new SqlServerDbContext())
            {
                //考虑到级联删除，如果使用基类方法仅attachcustomer的话ef会报错
                Customer customer = db.Customers.Include(x => x.Contacts).FirstOrDefault(x => x.Id == id);
                db.Customers.Remove(customer);
                var count = await db.SaveChangesAsync();
                return count > 0;
            }
        }

        public  async Task<bool> Remove(Customer customer)
        {
            using (var db = new SqlServerDbContext())
            {
                db.Customers.Attach(customer);
                foreach (var c in customer.Contacts)
                    db.CustomerContacts.Attach(c);
                db.Customers.Remove(customer);
                var count = await db.SaveChangesAsync();
                return count > 0;
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



        //public override async Task<bool> Update(Customer customer)
        //{
        //    using (var dc = new SqlServerDbContext())
        //    {
        //        dc.Configuration.ValidateOnSaveEnabled = false;
        //        var original = dc.Customers.Include(x => x.Contacts).Single(x => x.Id == customer.Id);

        //        //删除不存在的contact
        //        original.Contacts.Where(x => !customer.Contacts.Contains(x)).ToList().ForEach(x =>
        //          {
        //              dc.CustomerContacts.Attach(x);
        //              dc.Entry(x).State = EntityState.Deleted;
        //          });
        //        //添加新增的contact
        //        customer.Contacts.Where(x => x.Id == 0).ToList()
        //            .ForEach(x =>
        //            {
        //                dc.CustomerContacts.Add(x);
        //               // dc.Entry(x).State = EntityState.Added;

        //            });

        //        original.Contacts.ToList().ForEach(x =>
        //        {
        //            dc.Entry(x).State = EntityState.Detached;
        //        });


        //         dc.Entry(original).CurrentValues.SetValues(customer);

        //        var result = await dc.SaveChangesAsync();
        //        return result > 0;
        //    }
        //}

        


        public override async Task<bool> Update(Customer customer)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var exiting = dc.Customers.Include(x => x.Contacts).Single(x => x.Id == customer.Id);
          
          
                var addContacts = customer.Contacts.Where(x => x.Id == 0).ToList(); //此处一定要转成tolist！！
                var deletedContacts = exiting.Contacts.Where(x => !customer.Contacts.Contains(x)).ToList(); //此处一定要转成tolist！！

                var updatedContacts =exiting.Contacts.Where(x => customer.Contacts.Contains(x)).ToList();//此处一定要转成tolist！！

               
                addContacts.ForEach(t =>  //添加新建的
                {
                    t.Company = exiting; //此处必须要置上existing，否则，ef会重新创建一个customer
                    dc.Entry(t).State = EntityState.Added;
                });

                foreach (var t in updatedContacts) //更新
                {
                    var entry = dc.Entry(t);
                    entry.CurrentValues.SetValues(customer.Contacts.FirstOrDefault(x => x.Id == t.Id));
                    entry.State = EntityState.Modified;
                }

                deletedContacts.ToList().ForEach(t => dc.CustomerContacts.Remove(t)); //删除不存在的

                 dc.Entry(exiting).CurrentValues.SetValues(customer);

                var result = await dc.SaveChangesAsync();
                return result > 0;
                
            }
        }
    }
}
