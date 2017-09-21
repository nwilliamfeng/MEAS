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

        ///// <summary>
        ///// 检查两个引用的实体实例是否一致，如果不一样则变更
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="ds"></param>
        ///// <param name="target"></param>
        ///// <param name="source"></param>
        ///// <param name="complete"></param>
        //public static void ChangeReferenceIfNotEqual<T>(this DbSet<T> ds, T target, T source, Action complete)
        //  where T : class, IEntity
        //{
        //    if (target.Id == source.Id)//注意：此处不能用equal比较，即使是重写equal，因为有时ef传的是代理类对象，只能比较其id
        //        return;
        //    if (source.Id > 0)
        //        ds.Attach(source);
        //    else
        //        ds.Add(source);
        //    if (complete != null)
        //        complete();
        //}

        //public static void ChangeReferenceIfNotEqual<T>(this DbSet<T> ds,  T source)
        //  where T : class, IEntity
        //{

        //    if (source.Id > 0)
        //        ds.Attach(source);
        //    else
        //        ds.Add(source);

        //}

        //public static DbContext Check<T>(this DbContext dc,TorqueWrench target,TorqueWrench source)
        //{
        //    if (target.Id == source.Id)
        //        return dc;
        //    dc.GetDbSet<TorqueWrenchProduct>().ChangeReferenceIfNotEqual(target.Product , source.Product,()=>target.Product =source.Product);
        //    dc.GetDbSet<Customer>().ChangeReferenceIfNotEqual(target.Owner, source.Owner,()=>target.Owner=source.Owner);

        //    return dc;
        //}


        //public static DbContext CheckNavigationProperty<T>(this DbContext dc, T original ,T source,Action<T> afterCheck )
        //    where T:class,IEntity
        //{
        //    if (original.Id == source.Id)
        //        return dc;
        //    var exist = dc.GetDbSet<T>().Find(source.Id);
        //    if (exist != null)
        //    {
        //        afterCheck(exist);
        //    }
        //}


        //public static void CheckReference<T, X>(this DbSet<X> dc, Expression<Func<T, X>> exp, T source, T target)
        // where T : class, IEntity
        // where X : class, IEntity
        //{

        //    var me = exp.Body as MemberExpression;
        //    var pi = me.Member as PropertyInfo;

        //    var sourcePropertyValue = pi.GetValue(source, null) as X;
        //    var targetPropertyValue = pi.GetValue(target, null) as X;

        //    if (!targetPropertyValue.Equals(sourcePropertyValue))
        //    {
        //        if (source.Id > 0)
        //            dc.Attach(sourcePropertyValue);
        //        else
        //        {

        //            dc.Add(sourcePropertyValue);
        //        }
        //        pi.SetValue(target, sourcePropertyValue, null); //当新建时此设置无效

        //    }

        //}

        public static DbContext CheckReference<T, X>(this DbContext dc, Expression<Func<T, X>> exp, T source, T target )
        where T : class, IEntity
        where X : class, IEntity
        {
            //if (beforeCheck != null)
            //    beforeCheck();
            var me = exp.Body as MemberExpression;
            var pi = me.Member as PropertyInfo;

           
            var sourcePropertyValue = pi.GetValue(source, null) as X;
            var targetPropertyValue = pi.GetValue(target, null) as X;

            if (!targetPropertyValue.Equals(sourcePropertyValue))
            {
                if (sourcePropertyValue.Id > 0)
                    dc.Entry(sourcePropertyValue).State = EntityState.Unchanged;
                else
                {
                    dc.Entry(sourcePropertyValue).State = EntityState.Unchanged;
                    dc.Entry(sourcePropertyValue).State = EntityState.Added;
                }

                pi.SetValue(target, sourcePropertyValue);

            }


            return dc;
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
