using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;

namespace MEAS.Data
{
    public static class DbContextExtension
    {
        public static DbSet<T> GetDbSet<T>(this DbContext dc)
         where T : class
        {
            var type = dc.GetType().GetProperties().FirstOrDefault(x => x.PropertyType.Equals(typeof(DbSet<T>)));
            if (type != null)
                return type.GetValue(dc) as DbSet<T>;
            return null;
        }


        public static void SetDifferent<T>(this DbContext dc, T source, T target)
          where T : class, IEntity
        {
            if (!target.Equals(source))
            {
                if (source.Id > 0)
                    dc.GetDbSet<T>().Attach(source);
                else
                    dc.GetDbSet<T>().Add(source);
                Mapper.Map(source, target);
            }
        }
    }
}
