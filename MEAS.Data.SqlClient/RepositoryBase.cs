using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.SqlClient
{
    public abstract class RepositoryBase<T>
        where T:class,IEntity,new()
    {
        public virtual async Task<bool> Add(T entity)
        {

            using (var db = new SqlServerDbContext())
            {
                var ev = db.GetDbSet<T>().Add(entity);
                var count = await db.SaveChangesAsync();
                if (count > 0)
                {
                    entity.Id = ev.Id;
                    entity.Timestamp = ev.Timestamp;
                }

                return count >0;
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
                return count >0;
            }
        }

        public virtual async Task<bool> Update(T et)
        {
            using (var db = new SqlServerDbContext())
            {
                var ev = db.GetDbSet<T>().Attach(et);
                DbEntityEntry<T> entry = db.Entry(et);

                db.ObjectStateManager().ChangeObjectState(et, EntityState.Modified);
                var count = await db.SaveChangesAsync();
                if (count > 0)
                    et.Timestamp = ev.Timestamp;
                return count > 0;
            }
        }
    }
}
