using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MEAS.Data.MySql
{
    public class TorqueWrenchMeasureRepository :DbRepository, ITorqueWrenchMeasureRepository
    {   
        private string Col_tstcode = "TestCode";
        private string Col_tsterId = "TesterId";
        private string Col_tstDate = "TestDate";

        public TorqueWrenchMeasureRepository()
        {
          
        }

        public Task<bool> Delete(int id)
        {
            return Task.Run(() =>
            {
               
                return true;
            });
        }

        public Task<IEnumerable<TorqueWrenchMeasureDao>> FindWithCode(string code)
        {
            return Task.Run<IEnumerable<TorqueWrenchMeasureDao>>(() =>
            {
                return new TorqueWrenchMeasureDao[] { };
            });
        }

        public async Task<TorqueWrenchMeasureDao> FindWithId(int id)
        {
            //这里注意inner join和left join区别，前者需要满足两者条件，后者仅满足左表的条件    
            //query带的一个参数是splitOn，如果参数带上字段a，则从第一个字段开始比较，直到这个字段结束，之后字段信息会忽略，不返回。
            var sql = string.Format("select * from {0} t  join users u on t.testerid = u.id where t.id ={1}",TableName, id);
            var result = await this.CreateConnection().QueryAsync<TorqueWrenchMeasureDao, UserInfoDao, TorqueWrenchMeasureDao>(sql, (a, b) =>
            {
                a.Tester = b;
                return a;
            })  ;
       
            return result.FirstOrDefault();
        }

        public async Task<SearchResult<TorqueWrenchMeasureDao>> Find(DateTime start, DateTime end, int pagesize = 3, int pageIdx = 0)
        {

            var connection = this.CreateConnection();
            var sql = string.Format("select * from {0} t  join users u on t.testerid = u.id  LIMIT @a, @b  select count(*) from {0}", TableName);
            IEnumerable<TorqueWrenchMeasureDao> daos = null;
            using (var reader = connection.QueryMultiple(sql, new { a = pagesize * pageIdx, b = pagesize }))
            {
                daos = await reader.Read<TorqueWrenchMeasureDao,>();
                var total = await reader.ReadSingleAsync<int>();
            }

            var sql = string.Format("SELECT * FROM {0} LIMIT @a, @b", TableName);
                 //if (pageIdx < 0 || pagesize < 1)
                 //    return new SearchResult<TorqueWrenchMeasureDao>(new TorqueWrenchMeasureDao[] { },0);
                 //var daos = lst.Where(x => x.TestDate > start && x.TestDate < end).ToList();
                 //var data = daos.Skip(pagesize * (pageIdx)).Take(pagesize).ToList();
                 //foreach (var d in data)
                 //    System.Diagnostics.Debug.WriteLine(d);
                 //return new SearchResult<TorqueWrenchMeasureDao>(data, daos.Count);
            return new SearchResult<TorqueWrenchMeasureDao>(new TorqueWrenchMeasureDao[] { }, 0);
        }

        public async Task<bool> Add(TorqueWrenchMeasureDao measure)
        {

            var sql = string.Format("insert into {0} values(@a,@b,@c,@d)", this.TableName);
            var result = await this.CreateConnection().ExecuteAsync(sql, new { a = measure.Id, b = measure.TestCode, c = measure.Tester.Id, d = measure.TestDate })==1;
            if (result)
                measure.Id = this.GetInsertedId();
            return result;

        }

        public async Task<bool> Update(TorqueWrenchMeasureDao measure)
        {
            string sql = string.Format("UPDATE {0} SET {2} = @a, {3}=@b WHERE {1} = @{1}", TableName, ID, Col_tstcode, Col_tstDate);
            return await this.CreateConnection().ExecuteAsync(sql, new { a=measure.TestCode ,b=measure.TestDate }) > 0;
        }
    }
}
