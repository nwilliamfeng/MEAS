using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;

namespace MEAS.Data.SqlClient
{
    public class TorqueWrenchMeasureRepository :RepositoryBase<TorqueWrenchMeasure>, ITorqueWrenchMeasureRepository
    {



        //public async Task<bool> Remove(int id)
        //{
        //    using (var db = new SqlServerDbContext())
        //    {
        //      var test=  db.TorqueWrenchMeasures.Find(id);
        //     //   TorqueWrenchMeasureDao test = new TorqueWrenchMeasureDao { Id = id }; //这里不能简单创建指定id的实例，因为有引用的其他对象，ef会抛出relationship异常
        //          db.TorqueWrenchMeasures.Attach(test);

        //        db.TorqueWrenchMeasures.Remove(test);
        //        var count = await db.SaveChangesAsync();
        //        return count >0; //返回的不能简单的判断是否为1，如果有关联的对象，则删除数为1+关联数
        //    }
        //}

       public  Task<SearchResult<TorqueWrenchMeasure>> Find(string wrenchSN, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var total = db.TorqueWrenchMeasures
                   
                    .Where(x => x.Measurand.SerialNumber.Contains(wrenchSN)).Count();
                    var tests = db.TorqueWrenchMeasures
                    .Where(x => x.Measurand.SerialNumber.Contains(wrenchSN))
                    .OrderByDescending(x => x.Id)
                    .Skip(pagesize * pageIdx)
                    .Take(pagesize)
                    .Include(x => x.Environment)
                    .Include(x => x.Measurand.Product)
                    .Include(x => x.Measurand.Owner)
                    .ToList()
                    .Select(x => new TorqueWrenchMeasure { Id = x.Id, TestCode = x.TestCode, Environment = x.Environment, Measurand = x.Measurand });
                    
                    return new SearchResult<TorqueWrenchMeasure>(tests, total);
                }

            });
        }

        public Task<SearchResult<TorqueWrenchMeasure>> FindWithCode(string code, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    var total = db.TorqueWrenchMeasures.Where(x => x.TestCode != null && x.TestCode.Contains(code)).Count();
                    var tests = db.TorqueWrenchMeasures
                    .Where(x => x.TestCode != null && x.TestCode.Contains(code))
                    .OrderByDescending(x => x.Id)
                    .Skip(pagesize * pageIdx)
                    .Take(pagesize)
                    .Include(x=>x.Environment)
                    .Include(x=>x.Measurand)
                    .Select(x => new TorqueWrenchMeasure { Id = x.Id, TestCode = x.TestCode, Environment = x.Environment })
                    .ToList();
                    return new SearchResult<TorqueWrenchMeasure>(tests, total);
                }

            });          
        }

        public override Task<TorqueWrenchMeasure> Find(int id)
        {
            return Task.Run(() =>
            {
                using (var db = new SqlServerDbContext())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.TorqueWrenchMeasures
                    .Where(x => x.Id == id)
                    .Include(x => x.Measurand.Owner.Contacts)
                    .Include(x => x.Measurand.Product)
                    .Include(x => x.Standard)
                    .Include(x => x.Environment)
                    .FirstOrDefault()?.ToEntity();

                }
            });
        }

        public Task<SearchResult<TorqueWrenchMeasure>> Find(DateTime start, DateTime end, int pagesize = 3, int pageIdx = 0)
        {
            return Task.Run(() =>
            {
                return SearchResult<TorqueWrenchMeasure>.Empty;
            });
        }

         
        public override async  Task<bool>  Add(TorqueWrenchMeasure measure)
        {
            using (var dc = new SqlServerDbContext())
            {
                try
                {            
                    var dao = measure.ToDao();
                    if (dao.Environment.Id == 0)
                        dc.Environments.Add(dao.Environment);
                    if (dao.Measurand.Id == 0)
                        dc.TorqueWrenchs.Add(dao.Measurand);
                    if (dao.Standard.Id == 0)
                        dc.TorqueStandards.Add(dao.Standard);

                    dc.TorqueWrenchMeasures.Attach(dao);       //必须先attach，否则ef会自动插入新的userinfo而不是之前存在的userinfo         
                    var result = dc.TorqueWrenchMeasures.Add(dao);
                    var count =await dc.SaveChangesAsync();
               
                    if (count > 0)
                        AutoMapper.Mapper.Map(result, measure);
                    return count > 0;
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbEx.Dump();
                    throw dbEx;
                }
            }
        }



        //public override async Task<bool> Update(TorqueWrenchMeasure measure)
        //{
        //    using (var dc = new SqlServerDbContext())
        //    {
        //        var source = measure.ToDao();
        //        dc.Configuration.ValidateOnSaveEnabled = false;
        //         var original = dc.TorqueWrenchMeasures.Find(measure.Id);
        //        // source.Measurand.Owner.Contacts = null;

        //        //dc.TorqueWrenchs.Attach(source.Measurand);
        //        //original.Measurand = source.Measurand;

        //        var wrench = dc.TorqueWrenchs.Find(measure.Measurand.Id);
        //        if (wrench != null) 
        //            original.Measurand = wrench;
        //        else
        //        {
        //            source.Measurand.Owner.Contacts = null;                   
        //            dc.TorqueWrenchs.Attach(source.Measurand);
        //            dc.TorqueWrenchs.Add(source.Measurand);
        //            original.Measurand = source.Measurand;
        //        }
        //        //    dc.TorqueWrenchs.ChangeReferenceIfNotEqual(original.Measurand, source.Measurand, () => original.Measurand = source.Measurand);


        //        dc.TorqueWrenchMeasures.Attach(original);


        //        var entry = dc.Entry(original);

        //        entry.State = EntityState.Modified;


        //        entry.CurrentValues.SetValues(source);

        //        Console.WriteLine("entry "+entry.Entity.Measurand?.SerialNumber);



        //        //   dc.TorqueWrenchs.ChangeReferenceIfNotEqual(original.Measurand, source.Measurand, () => original.Measurand = source.Measurand);

        //     //   dc.Environments.ChangeReferenceIfNotEqual(original.Environment, source.Environment, () => original.Environment = source.Environment);
        //    //    dc.TorqueStandards.ChangeReferenceIfNotEqual(original.Standard, source.Standard, () => original.Standard = source.Standard);



        //        //dc.Entry(original).CurrentValues.SetValues(source);

        //        var result = await dc.SaveChangesAsync();

        //        if (result > 0)
        //        {
        //            Mapper.Map(source.ToEntity(), measure);
        //        }
        //        return result > 0;
        //    }
        //}

        public  void DumpTimestamp( byte[] timestamp)
        {
            string s = null;
            foreach (var t in timestamp)
                s += t.ToString();
            Console.WriteLine( "version:"+ s);
        }

        public override async Task<bool> Update(TorqueWrenchMeasure measure)
        {
            using (var dc = new SqlServerDbContext())
            {
                
                var source = measure.ToDao();
                this.DumpTimestamp(source.Timestamp);
                dc.Configuration.ValidateOnSaveEnabled = false;
                var original = dc.TorqueWrenchMeasures.Find(measure.Id);

                //var wrench = dc.TorqueWrenchs.Find(measure.Measurand.Id);
                //if (wrench != null)
                //    original.Measurand = wrench;
                //else
                //{
                //    source.Measurand.Owner.Contacts = null;
                //    dc.TorqueWrenchs.Attach(source.Measurand);
                //    dc.TorqueWrenchs.Add(source.Measurand);
                //    original.Measurand = source.Measurand;
                //}


                //  dc.TorqueWrenchMeasures.Attach(original);



                dc.Entry(original).CurrentValues.SetValues(source);

                dc.Environments.CheckReference<TorqueWrenchMeasureDao, Environment>(x => x.Environment, source, original);


                //   dc.TorqueWrenchs.ChangeReferenceIfNotEqual(original.Measurand, source.Measurand, () => original.Measurand = source.Measurand);

                //   dc.Environments.ChangeReferenceIfNotEqual(original.Environment, source.Environment, () => original.Environment = source.Environment);
                //    dc.TorqueStandards.ChangeReferenceIfNotEqual(original.Standard, source.Standard, () => original.Standard = source.Standard);



                //dc.Entry(original).CurrentValues.SetValues(source);

                var result = await dc.SaveChangesAsync();

                if (result > 0)
                {
                    Mapper.Map(source.ToEntity(), measure);
                }
                return result > 0;
            }
        }
    }
}
