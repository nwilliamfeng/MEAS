using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MEAS.Data.SqlClient
{
    public abstract class RepositoryBase<T>
        where T : class, IEntity, new()
    {
        static RepositoryBase()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToDaoMappingProfile>();
                x.AddProfile<DaoToEntityMappingProfile>();
                x.CreateMap<TorqueWrench, TorqueWrench>();
                x.CreateMap<Customer, Customer>();
                x.CreateMissingTypeMaps = true; //支持匿名类型
            });

        }

        public virtual async Task<bool> Add(T entity)
        {

            using (var db = new SqlServerDbContext())
            {
                try
                {
                    var ev = db.GetDbSet<T>().Add(entity);
                    var count = await db.SaveChangesAsync();

                    return count > 0;
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbEx.Dump();
                    throw dbEx;
                }
            }
        }

        public virtual async Task<T> Find(int id)
        {
            using (var db = new SqlServerDbContext())
            {
                return await db.GetDbSet<T>().FindAsync(id);

            }
        }



        public virtual async Task<bool> Remove(int id)
        {
            using (var db = new SqlServerDbContext())
            {
                T ev = new T { Id = id };
                var ds = db.GetDbSet<T>();
                ds.Attach(ev);
                ds.Remove(ev);
                var count = await db.SaveChangesAsync();
                return count > 0;
            }
        }

        public virtual async Task<bool> Update(T et)
        {
            using (var db = new SqlServerDbContext())
            {
                try
                {
                    var ev = db.GetDbSet<T>().Attach(et);
                    db.Entry(et).State = EntityState.Modified;
                    return await db.SaveChangesAsync() > 0;
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbEx.Dump();
                    throw dbEx;
                }
            }
        }

      
    }
}
