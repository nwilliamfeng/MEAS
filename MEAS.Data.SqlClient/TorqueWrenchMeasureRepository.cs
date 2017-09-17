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
    public class TorqueWrenchMeasureRepository :ITorqueWrenchMeasureRepository
    {   
        private IEnvironmentRepository EnvironmentRepository { get; set; }

        public TorqueWrenchMeasureRepository(IEnvironmentRepository environmentRepository)
        {
            this.EnvironmentRepository = environmentRepository;
        }

        public async Task<bool> Delete(int id)
        {
            using (var db = new SqlServerDbContext())
            {
              var test=  db.TorqueWrenchMeasures.Find(id);
             //   TorqueWrenchMeasureDao test = new TorqueWrenchMeasureDao { Id = id }; //这里不能简单创建指定id的实例，因为有引用的其他对象，ef会抛出relationship异常
                  db.TorqueWrenchMeasures.Attach(test);
             
                db.TorqueWrenchMeasures.Remove(test);
                var count = await db.SaveChangesAsync();
                return count >0; //返回的不能简单的判断是否为1，如果有关联的对象，则删除数为1+关联数
            }
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
                    .Select(x => new TorqueWrenchMeasure { Id = x.Id, TestCode = x.TestCode, Environment = x.Environment })
                    .ToList();
                    return new SearchResult<TorqueWrenchMeasure>(tests, total);
                }

            });          
        }

        public  Task<TorqueWrenchMeasure> FindWithId(int id)
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

        public async Task<bool> Add(TorqueWrenchMeasure measure)
        {

            using (var dc = new SqlServerDbContext())
            {
                try
                {
                    if (measure.Environment == null)
                        throw new ArgumentNullException("环境参数为空。");
                    else if (measure.Environment.Id == 0)
                    {
                        await this.EnvironmentRepository.Add(measure.Environment);
                       // measure.Environment = await this.EnvironmentRepository.Find(measure.Environment.Id);
                    }
                    //todo 其他引用属性都参考上述操作
                   
                    var entry = dc.TorqueWrenchMeasures.Attach(measure.ToDao());       //必须先attach，否则ef会自动插入新的userinfo而不是之前存在的userinfo         
                    var result = dc.TorqueWrenchMeasures.Add(entry);
                    var count = dc.SaveChanges();
                    measure.Id = result.Id;
                    measure.Timestamp = result.Timestamp;
                    if (count > 0)
                    {
                        AutoMapper.Mapper.Map(result, measure);
                    }
                    return count > 0;
                }
                catch (DbEntityValidationException dbEx)
                {
                    dbEx.Dump();
                    throw dbEx;
                }
            }
        }

        public async Task<bool> Update(TorqueWrenchMeasure measure)
        {
            using (var dc = new SqlServerDbContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var original = dc.TorqueWrenchMeasures.Include(x=> x.Environment) .Single(x=>x.Id == measure.Id);
                var curr = measure.ToDao();
                if (!original.Environment.Equals(measure.Environment)) //如果所指向的引用有变更(即id不同)，则赋值
                {
                    dc.Environments.Attach(curr.Environment); //如果不attach，则ef会让db自动添加一个实例
                    original.Environment = curr.Environment;
                }
                //todo -- 其他相关引用类型的属性都参考enviroment设置
                dc.Entry(original).CurrentValues.SetValues(curr);
                var result=await  dc.SaveChangesAsync();
               
                return result > 0;
            }
        }
    }
}
