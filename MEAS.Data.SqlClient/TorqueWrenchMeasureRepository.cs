using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


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
                    .Select(x => new TorqueWrenchMeasure { Id = x.Id, TestCode = x.TestCode, Environment = x.Environment })
                    .ToList();
                    return new SearchResult<TorqueWrenchMeasure>(tests, total);
                }

            });          
        }

        public override Task<TorqueWrenchMeasure> Find(int id)
        {
            return Task.Run(()=>
            {
                using (var db =new SqlServerDbContext())
                {
                    return db.TorqueWrenchMeasures
                    .Where(x => x.Id == id)
                    .Include(x=>x.Environment)
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
                    if (dao.Setting.Id == 0)
                        dc.TorqueWrenchMeasureSettings.Add(dao.Setting);
                    var entry = dc.TorqueWrenchMeasures.Attach(dao);       //必须先attach，否则ef会自动插入新的userinfo而不是之前存在的userinfo         
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

        public override async Task<bool> Update(TorqueWrenchMeasure measure)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var original = dc.TorqueWrenchMeasures
                    .Include(x => x.Environment)
                    .Include(x => x.Setting)
                    .FirstOrDefault(x => x.Id == measure.Id);
                var source = measure.ToDao();
                if (!original.Environment.Equals(source.Environment)) //如果所指向的引用有变更(即id不同)，则赋值
                {
                    if (source.Environment.Id > 0)
                        dc.Environments.Attach(source.Environment); //如果不attach，则ef会让db自动添加一个实例
                   else
                        dc.Environments.Add(source.Environment);
                    original.Environment = source.Environment;
                }
             
                if (!original.Setting.Equals(source.Setting))  
                {
                    if (source.Setting.Id > 0) 
                        dc.TorqueWrenchMeasureSettings.Attach(source.Setting);
                    else
                        dc.TorqueWrenchMeasureSettings.Add(source.Setting);
                    original.Setting = source.Setting;
                }
                dc.Entry(original).CurrentValues.SetValues(source);
                var result = await dc.SaveChangesAsync();
                if (result > 0)
                    AutoMapper.Mapper.Map(original, measure);
                return result > 0;
            }
        }
    }
}
