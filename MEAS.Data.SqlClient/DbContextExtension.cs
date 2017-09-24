using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;
using AutoMapper;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq.Expressions;

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


        //public static DbContext UpdateNavigationProperty<T, X>(this DbContext dc, Expression<Func<T, X>> exp, T source, T target ,Action beforeUpdate=null)
        //where T : class, IEntity
        //where X : class, IEntity
        //{
        //    if (beforeUpdate != null)
        //        beforeUpdate();
        //    var me = exp.Body as MemberExpression;
        //    var pi = me.Member as PropertyInfo;

        //    var sourcePropertyValue = pi.GetValue(source, null) as X;
        //    var targetPropertyValue = pi.GetValue(target, null) as X;
        //    dc.Entry(targetPropertyValue).State = EntityState.Detached; //旧对象必须要detached，否则新增时部分复合实体ef会抛异常

        //    if (!targetPropertyValue.Equals(sourcePropertyValue))
        //    {
        //        if (sourcePropertyValue.Id > 0)
        //            dc.Entry(sourcePropertyValue).State = EntityState.Unchanged;
        //        else
        //        {
        //            dc.Entry(sourcePropertyValue).State = EntityState.Unchanged;                  
        //           dc.Entry(sourcePropertyValue).State = EntityState.Added;
        //        }         
        //        pi.SetValue(target, sourcePropertyValue);
        //    }
        //    return dc;
        //}

        public static DbContext UpdateNavigationProperty<T, X>(this DbContext dc, Expression<Func<T, X>> exp, T source, T target, Action beforeUpdate = null)
              where T : class, IEntity
              where X : class, IEntity
        {
            if (beforeUpdate != null)
                beforeUpdate();
            var me = exp.Body as MemberExpression;
            var pi = me.Member as PropertyInfo;

            var sourcePropertyValue = pi.GetValue(source, null) as X;
            var targetPropertyValue = pi.GetValue(target, null) as X;
            dc.Entry(targetPropertyValue).State = EntityState.Detached; //旧对象必须要detached，否则新增时部分复合实体ef会抛异常

            if (!targetPropertyValue.Equals(sourcePropertyValue))
            {
                if (sourcePropertyValue.Id > 0)
                    dc.Entry(sourcePropertyValue).State = EntityState.Unchanged;
                else
                    dc.DoAdd(sourcePropertyValue);
                pi.SetValue(target, sourcePropertyValue);
            }
            return dc;
        }

        private static void DoAdd(this DbContext dc, object obj)
        {
         
            dc.Entry(obj).State = EntityState.Unchanged;
            dc.Entry(obj).State = EntityState.Added;

            obj.GetType()
                .GetProperties()
                .Where(x =>typeof(IEntity).IsAssignableFrom(x.PropertyType))
                .Select(x => x.GetValue(obj, null))
                .OfType<IEntity>()
                .Where(x=>x.Id==0)
                .ToList()
                .ForEach(x => DoAdd(dc, x));
        }

        public static ObjectStateManager ObjectStateManager(this DbContext dc)
        {
            return ((IObjectContextAdapter)dc).ObjectContext.ObjectStateManager;
        }


        public static void Dump(this DbEntityValidationException ex)
        {
            foreach (var validationErrors in ex.EntityValidationErrors) //打印错误
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    System.Console.WriteLine("Property: {0} Error: {1}" +
                                            validationError.PropertyName +
                                            validationError.ErrorMessage);
                }
            }
        }
    }
}
